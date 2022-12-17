using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HireSort.Controllers
{
    [Route("api/resume")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private IResumeParsing _resumeParsing;

        public ResumeController(IResumeParsing resumeParsing)
        {
            _resumeParsing = resumeParsing;
        }

        [HttpPost]
        [Route("check-resume-compatibility")]
        public async Task<string> GetResumeContent([FromQuery] int resumeId, int jobId)
        {
            return await _resumeParsing.resumeCheckCompatibility(resumeId, jobId);
        }
        [HttpGet]
        [Route("check-resume-compatibility")]
        public async Task<string> GetResumeCompatibility([FromQuery] int resumeId, int jobId)
        {
            return await _resumeParsing.resumeGetCompatibility(resumeId, jobId);
        }


    }
}
