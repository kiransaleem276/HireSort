using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class ViewAllResumeController : Controller
    {
        private readonly ILogger<ViewAllResumeController> _logger;

        public ViewAllResumeController(ILogger<ViewAllResumeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ViewAllResume()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}