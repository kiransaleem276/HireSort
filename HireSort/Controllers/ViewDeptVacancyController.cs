using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{
    public class ViewDeptVacancyController : Controller
    {
        private readonly ILogger<ViewDeptVacancyController> _logger;

        public ViewDeptVacancyController(ILogger<ViewDeptVacancyController> logger)
        {
            _logger = logger;
        }

        public IActionResult ViewDeptVacancy()
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