using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class ViewJobDetailController : Controller
    {
        private readonly ILogger<ViewJobDetailController> _logger;

        public ViewJobDetailController(ILogger<ViewJobDetailController> logger)
        {
            _logger = logger;
        }

        public IActionResult ViewJobDetail()
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