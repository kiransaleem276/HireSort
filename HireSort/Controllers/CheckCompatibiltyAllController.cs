using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class CheckCompatibiltyAllController : Controller
    {
        private readonly ILogger<CheckCompatibiltyAllController> _logger;

        public CheckCompatibiltyAllController(ILogger<CheckCompatibiltyAllController> logger)
        {
            _logger = logger;
        }

        public IActionResult CheckCompatibiltyAll()
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