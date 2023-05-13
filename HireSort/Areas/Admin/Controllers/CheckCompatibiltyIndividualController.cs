using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Areas.Admin.Controllers
{
    public class CheckCompatibiltyIndividualController : Controller
    {
        private readonly ILogger<CheckCompatibiltyIndividualController> _logger;

        public CheckCompatibiltyIndividualController(ILogger<CheckCompatibiltyIndividualController> logger)
        {
            _logger = logger;
        }

        public IActionResult CheckCompatibiltyIndividual()
        {
            return View();
        }
        //hfddd
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}