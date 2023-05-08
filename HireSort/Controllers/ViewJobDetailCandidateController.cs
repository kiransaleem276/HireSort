using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class ViewJobDetailCandidateController : Controller
    {
        private readonly ILogger<ViewJobDetailCandidateController> _logger;

        public ViewJobDetailCandidateController(ILogger<ViewJobDetailCandidateController> logger)
        {
            _logger = logger;
        }

        public IActionResult ViewJobDetailCandidate()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}