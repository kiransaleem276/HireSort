using HireSort.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireSort.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResumeViewController : ControllerBase
	{
		private readonly IResumeParsing _resumeParsing;

		public ResumeViewController(IResumeParsing resumeParsing)
		{
			_resumeParsing = resumeParsing;
		}

		[HttpPost]
		[Route("uploadfile")]

		public Task UploadFiles(IFormFile file, int jobId)
		{
			if (file.Length > 0)
			{
				var result = _resumeParsing.ResumeUpload(file, jobId);
			}
			return Task.CompletedTask;
		}
	}
}
