using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class HomeCandidateController : Controller
    {
        private readonly ILogger<HomeCandidateController> _logger;
        private IDashboard _dashboard;
        private ApiResponseMessage apiResponseMessage = null;

        public HomeCandidateController(ILogger<HomeCandidateController> logger, IDashboard dashboard)
        {
            _logger = logger;
            _dashboard = dashboard;
        }

        public IActionResult IndexCandidate()
        {
            return View();
        }

        public IActionResult PrivacyCandidate()
        {
            return View();
        }

        //public IActionResult AddNewJob()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}