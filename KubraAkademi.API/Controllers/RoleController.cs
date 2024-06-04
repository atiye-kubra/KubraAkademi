using AutoMapper;
using KubraAkademi.API.Dtos;
using KubraAkademi.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace KubraAkademi.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = _context.Users
                .Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new { user, userRole })
                .Join(_context.Roles, joined => joined.userRole.RoleId, role => role.Id, (joined, role) => new { joined.user, role })
                .Select(x => new
                {
                    UserId = x.user.Id,
                    Username = x.user.UserName,
                    RoleId = x.role.Id,
                    RoleName = x.role.Name
                })
                .Where(e => e.UserId != currentUserId)
                .ToList();


            var roles = _context.Roles.ToList();

            var response = new
            {
                Users = users,
                Roles = roles
            };

            return Ok(response);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers(string userId, [FromBody] UpdateUserRoleDto req)
        {
            var userRole = _context.UserRoles.FirstOrDefault(e => e.UserId == userId);


            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            var role = _roleManager.FindByIdAsync(req.RoleId).Result;
            if (role == null)
            {
                return NotFound("Rol bulunamadı");
            }

            var existingRoles = _userManager.GetRolesAsync(user).Result;
            _ = _userManager.RemoveFromRolesAsync(user, existingRoles).Result;

            // Add new role
            _ = _userManager.AddToRoleAsync(user, role.Name).Result;



            //userRole.RoleId = req.RoleId;

            //_context.UserRoles.Update(userRole);
            //_context.SaveChanges();

            return Ok();
        }
    }
}
