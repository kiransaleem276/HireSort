using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HireSort.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ViewAllResumeController : ControllerBase
    {
        private readonly IResumeParsing _resumeParsing;

        //private readonly ILogger<HomeController> _logger;

        public ViewAllResumeController(IResumeParsing resumeParsing)
        {
            _resumeParsing = resumeParsing;
        }

        //public IActionResult ViewAllResume()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        [HttpPost]
        [Route("uploadfile")]

        public Task UploadFiles(IFormFile file, int jobId)
        {
            //foreach (IFormFile file in files)
            //{
            if (file.Length > 0)
            {
                var result = _resumeParsing.ResumeUpload(file, jobId);
            }
            return Task.CompletedTask;
        }
    }
}


//using HireSort.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;

//namespace HireSort.Controllers
//{
//    public class ViewAllResumeController : Controller
//    {
//        private readonly ILogger<ViewAllResumeController> _logger;

//        public ViewAllResumeController(ILogger<ViewAllResumeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult ViewAllResume()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}