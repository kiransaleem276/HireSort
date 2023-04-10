//using HireSort.Repository.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace HireSort.Controllers
//{
//    [Route("api/resume")]
//    [ApiController]
//    public class ResumeController : ControllerBase
//    {
//        private IResumeParsing _resumeParsing;

//        public ResumeController(IResumeParsing resumeParsing)
//        {
//            _resumeParsing = resumeParsing;
//        }

//        [HttpPost]
//        [Route("check-resume-compatibility")]
//        public async Task<string> GetResumeContent([FromQuery] int resumeId, int jobId)
//        {
//            return await _resumeParsing.resumeCheckCompatibility(resumeId, jobId);
//        }
//        //[HttpGet]
//        //[Route("check-resume-compatibility")]
//        ////public async Task<string> GetResumeCompatibility([FromQuery] int resumeId, int jobId)
//        ////{
//        ////    //return await _resumeParsing.resumeGetCompatibility(resumeId, jobId);
//        ////}


//    }
//}

using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{

    public class ResumeController : Controller
    {
        private readonly ILogger<ResumeController> _logger;
        private IDashboard _dashboard;
        public ResumeController(ILogger<ResumeController> logger, IDashboard dashboard)
        {
            _logger = logger;
            _dashboard = dashboard;
        }

        public IActionResult ViewAllResume()
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
