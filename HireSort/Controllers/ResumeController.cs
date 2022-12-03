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

        [HttpGet]
        [Route("resume-content")]
        public async Task<string> GetResumeContent([FromQuery] int jobId)
        {
            return await _resumeParsing.resumeContent(jobId);
        }

    }
}
