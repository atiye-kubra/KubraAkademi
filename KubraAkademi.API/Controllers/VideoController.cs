using AutoMapper;
using KubraAkademi.API.Dtos;
using KubraAkademi.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KubraAkademi.API.Controllers
{
    [Route("api/video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public VideoController(UserManager<AppUser> userManager, AppDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetCategoriesByExamPath(string exam, string category)
        {
            var existingExam = _context.Exams.FirstOrDefault(e => e.Path == exam);

            if (existingExam == null)
            {
                return NotFound();
            }

            var existingCategory = _context.Categories.FirstOrDefault(e => e.ExamId == existingExam.Id && e.Path == category);

            if (existingCategory == null)
            {
                return NotFound();
            }

            var videos = _context.Videos.Where(e => e.CategoryId == existingCategory.Id).ToList();

            var response = new GetCategoriesByExamPathResponseDto
            {
                CategoryTitle = existingCategory.Title,
                ExamTitle = existingExam.Title,
                Videos = videos,
                Description = existingCategory.Description
            };

            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetVideoById(Guid id)
        {
            var existingVideo = _context.Videos
                .FirstOrDefault(e => e.Id == id);

            if (existingVideo == null)
            {
                return NotFound();
            }

            return Ok(existingVideo);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddVideo(AddVideoDto req)
        {
            var isVideoExist = _context.Videos.Any(e => e.EmbedUrl == req.EmbedUrl);
            if (isVideoExist)
            {
                return BadRequest();
            }

            var user = _userManager.GetUserAsync(User).Result;

            var video = new Video
            {
                Title = req.Title,
                CategoryId = req.CategoryId,
                EmbedUrl = req.EmbedUrl,
                Description = req.Description,
                CreatedBy = user.FullName,
                IsActive = req.IsActive,
            };

            _context.Videos.Add(video);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{videoId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(Guid videoId, UpdateVideoDto req)
        {
            var video = _context.Videos.FirstOrDefault(e => e.Id == videoId);

            if (video == null)
            {
                return BadRequest();
            }

            video.Title = req.Title;
            video.EmbedUrl = req.EmbedUrl;
            video.IsActive = req.IsActive;
            video.Description = req.Description;

            _context.Videos.Update(video);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{videoId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteVideo(Guid videoId)
        {
            var video = _context.Videos.FirstOrDefault(e => e.Id == videoId);
            if (video == null)
            {
                return BadRequest();
            }

            _context.Videos.Remove(video);
            _context.SaveChanges();

            return Ok();
        }
    }
}
