using AutoMapper;
using KubraAkademi.API.Dtos;
using KubraAkademi.API.Helper;
using KubraAkademi.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KubraAkademi.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<CategoryDto> GetList() // id'ye bakmadan tüm ürün listesini döner
        {
            var categories = _context.Categories.ToList();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public CategoryDto Get(int id) // id'si verilen Category'ye göre tek satır döner
        {
            var category = _context.Categories.Where(s => s.Id == id).SingleOrDefault();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        [HttpGet("by-id")]
        public IActionResult GetCategoriesByExamId(int id)
        {
            var result = _context.Categories.Where(e => e.ExamId == id).ToList();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory(AddCategoryDto req)
        {
            var isExamExists = _context.Exams.Any(e => e.Id == req.ExamId);

            if (!isExamExists)
            {
                return BadRequest();
            }

            var isCategoryExists = _context.Categories.Any(e => e.ExamId == req.ExamId && e.Title == req.Title);

            if (isCategoryExists)
            {
                return BadRequest();
            }

            var category = new Category
            {
                ExamId = req.ExamId,
                Title = req.Title,
                Description = req.Description,
                Path = PathHelper.ConvertToUrl(req.Title)
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCategory(int categoryId, UpdateCategoryDto req)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == categoryId);

            if (category == null)
            {
                return BadRequest();
            }

            category.Description = req.Description;
            category.Title = req.Title;
            category.Path = PathHelper.ConvertToUrl(req.Title);

            _context.Categories.Update(category);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == categoryId);
            if (category == null)
            {
                return BadRequest();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }
    }
}
