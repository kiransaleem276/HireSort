using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class AddNewJobController : Controller
    {
        private readonly ILogger<AddNewJobController> _logger;

        public AddNewJobController(ILogger<AddNewJobController> logger)
        {
            _logger = logger;
        }

        public IActionResult AddNewJob()
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