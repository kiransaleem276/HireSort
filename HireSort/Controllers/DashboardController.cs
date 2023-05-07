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
            var result = await _dashboard.GetDepartment();
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpGet]
        [Route("vacancies-department-wise")]
        public async Task<IActionResult> GetVacanciesDepartmentWise([FromQuery] int departId)
        {
            var result = await _dashboard.GetVacanciesDepartmentWise(departId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("department-and-vacancies-details")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int departId, int vacancyId)
        {
            var result = await _dashboard.GetDepartAndVacacyDetails(departId, vacancyId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("resume-list")]
        public async Task<IActionResult> GetDepartAndVacancyDetails([FromQuery] int departId, int vacancyId, bool isShortListedResume = false)
        {
            var result = await _dashboard.GetAllResumes(departId, vacancyId, isShortListedResume);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("depart-vacancy-count")]
        public async Task<IActionResult> GetDepartmentVacancyCount()
        {
            var result = await _dashboard.GetDepartmentVacancyCount();
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("depart-vacancy-list")]
        public async Task<IActionResult> GetDepartmentJobs([FromQuery] int departId)
        {
            var result = await _dashboard.GetDepartmentJobs(departId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("job-detail")]
        public async Task<IActionResult> GetJobDetail([FromQuery] int departId, int jobId)
        {
            var result = await _dashboard.GetJobDetail(departId, jobId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("resume-compatibitlity")]
        public async Task<IActionResult> GetResumeCompatibiltiy([FromQuery] int resumeId, int jobId)
        {
            var result = await _dashboard.GetResumeCompatibiltiy(resumeId, jobId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
                if (result.Result.StatusCode == 400)
                {
                    return BadRequest(result);
                }
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
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        [HttpPost]
        [Route("resume-shorlisting")]
        public async Task<IActionResult> ResumeShortlisting(int resumeId)
        {
            var result = await _dashboard.ResumeShorlisting(resumeId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("test")]
        public async Task<IActionResult> Test(IFormFile files,int jobId)
        {
            if (files.Length > 0)
            {
                var result = _resumeParsing.ResumeUpload(files, jobId);
                if (result.Result.StatusCode == 400)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string email,string password)
        {
            var result = _dashboard.Login(email, password);
            if (result.Result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

    }
}
