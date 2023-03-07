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
        private readonly IResumeParsing _resumeParsing;
        public DashboardController(ILogger<HomeController> logger, IDashboard dashboard, IResumeParsing resumeParsing)
        {
            _logger = logger;
            _dashboard = dashboard;
            _resumeParsing = resumeParsing;
        }

        [HttpGet]
        [Route("departments")]
        public async Task<IActionResult> GetDepartment()
        {
            var response = await _dashboard.GetDepartment();
            return Ok(response);

        }

        [HttpGet]
        [Route("vacancies-department-wise")]
        public async Task<IActionResult> GetVacanciesDepartmentWise([FromQuery] int departId)
        {
            var response = await _dashboard.GetVacanciesDepartmentWise(departId);
            return Ok(response);
        }
        [HttpGet]
        [Route("department-and-vacancies-details")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int departId, int vacancyId)
        {
            var response = await _dashboard.GetDepartAndVacacyDetails(departId, vacancyId);
            return Ok(response);
        }
        [HttpGet]
        [Route("resume-list")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int departId, int vacancyId, bool isShortListedResume = false)
        {
            var response = await _dashboard.GetAllResumes(departId, vacancyId, isShortListedResume);
            return Ok(response);
        }
        [HttpGet]
        [Route("depart-vacancy-count")]
        public async Task<IActionResult> GetDepartmentVacancyCount()
        {
            var response = await _dashboard.GetDepartmentVacancyCount();
            return Ok(response);
        }
        [HttpGet]
        [Route("depart-vacancy-list")]
        public async Task<IActionResult> GetDepartmentJobs([FromQuery] int departId)
        {
            var response = await _dashboard.GetDepartmentJobs(departId);
            return Ok(response);
        }
        [HttpGet]
        [Route("job-detail")]
        public async Task<IActionResult> GetJobDetail([FromQuery] int departId, int jobId)
        {
            var response = await _dashboard.GetJobDetail(departId, jobId);
            return Ok(response);
        }
        [HttpGet]
        [Route("resume-compatibitlity")]
        public async Task<IActionResult> GetResumeCompatibiltiy([FromQuery] int resumeId, int jobId)
        {
            var response = await _dashboard.GetResumeCompatibiltiy(resumeId, jobId);
            return Ok(response);
        }
        [HttpPost]
        [Route("uploadfile")]

        public async Task<IActionResult> UploadFiles(IFormFile file, int jobId)
        {
            //foreach (IFormFile file in files)
            //{
            if (file.Length > 0)
            {
                var result = _resumeParsing.ResumeUpload(file, jobId);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("check-resume-compatibility")]
        public async Task<IActionResult> GetResumeContent([FromQuery] int resumeId, int jobId)
        {
            //var result = await _resumeParsing.resumeCheckCompatibility(resumeId, jobId);
            var result = await _resumeParsing.resumeCheckCompatibility(resumeId, jobId);
            return Ok(result);

        }
        //[HttpPost]
        //public async Task<IActionResult> Test(IFormFile data)
        //{
        //    return "test";
        //}
        [HttpPost]
        [Route("test")]
        public async Task<IActionResult> Test(IFormFile files,int jobId)
        {
            if (files.Length > 0)
            {
                var result = _resumeParsing.ResumeUpload(files, jobId);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
