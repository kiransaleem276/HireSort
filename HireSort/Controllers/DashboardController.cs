using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireSort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private IDashboard _dashboard;
        private ApiResponseMessage apiResponseMessage = null;
        public DashboardController(ILogger<HomeController> logger, IDashboard dashboard)
        {
            _logger = logger;
            _dashboard = dashboard;
        }

        [HttpGet]
        [Route("departments")]
        public async Task<IActionResult> GetDepartment([FromQuery] int clientId)
        {
            var response = await _dashboard.GetDepartment(clientId);
            return Ok(response);

        }

        [HttpGet]
        [Route("vacancies-department-wise")]
        public async Task<IActionResult> GetVacanciesDepartmentWise([FromQuery] int clientId, int departId)
        {
            var response = await _dashboard.GetVacanciesDepartmentWise(clientId, departId);
            return Ok(response);
        }
        [HttpGet]
        [Route("department-and-vacancies-details")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int clientId)
        {
            var response = await _dashboard.GetDepartAndVacacyDetails(clientId);
            return Ok(response);
        }

    }
}
