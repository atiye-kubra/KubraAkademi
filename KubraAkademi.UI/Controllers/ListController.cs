using KubraAkademi.UI.Models.List;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata;

namespace KubraAkademi.UI.Controllers
{
    public class ListController : Controller
    {
        private readonly IConfiguration _configuration;
        public ListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index(string exam, string category)
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;

            var vm = new ListIndexViewModel
            {
                Exam = exam,
                Category = category,
               
            };

            return View(vm);
        }

        public IActionResult Video(string exam, string category, Guid videoId)
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;

            var vm = new ListVideoViewModel
            {
                VideoId = videoId,
            };


            return View(vm);
        }

        public IActionResult DocumentViewer()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }
    }
}
