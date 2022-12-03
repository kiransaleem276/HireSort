﻿using HireSort.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}