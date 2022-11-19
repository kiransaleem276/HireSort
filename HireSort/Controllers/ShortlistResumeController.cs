using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class ShortlistResumeController : Controller
    {
        private readonly ILogger<ShortlistResumeController> _logger;

        public ShortlistResumeController(ILogger<ShortlistResumeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ShortlistResume()
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