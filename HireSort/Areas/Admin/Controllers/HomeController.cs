using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace HireSort.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDashboard _dashboard;
        private ApiResponseMessage apiResponseMessage = null;

        public HomeController(ILogger<HomeController> logger, IDashboard dashboard)
        {
            _logger = logger;
            _dashboard = dashboard;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult AddNewJob()
        //{
        //    retfdsssddddssssssssssssurndddd View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}