using AutoMapper;
using KubraAkademi.API.Dtos;
using KubraAkademi.API.Helper;
using KubraAkademi.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KubraAkademi.API.Controllers
{
    [Route("api/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ExamController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetExamAndCategories()
        {
            var exams = _context.Exams
                .Include(e => e.Categories)
                .OrderBy(e => e.Title)
                .ToList();

            return Ok(exams);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddExam(AddExamDto req)
        {
            var isExamExist = _context.Exams.Any(e => e.Title == req.Title);
            if (isExamExist)
            {
                return BadRequest();
            }

            var urlPath = PathHelper.ConvertToUrl(req.Title);

            var exam = new Exam
            {
                Title = req.Title,
                Path = urlPath,
            };

            _context.Exams.Add(exam);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{examId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateExam(int examId, UpdateExamDto req)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == examId);

            if (exam == null)
            {
                return BadRequest();
            }

            exam.Title = req.Title;
            exam.Path = PathHelper.ConvertToUrl(req.Title);

            _context.Exams.Update(exam);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete("{examId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteExam(int examId)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == examId);
            if (exam == null)
            {
                return BadRequest();
            }

            _context.Exams.Remove(exam);
            _context.SaveChanges();

            return Ok();
        }

    }
}
