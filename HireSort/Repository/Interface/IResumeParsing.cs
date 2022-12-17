namespace HireSort.Repository.Interface
{
    public interface IResumeParsing
    {
        public Task<string> resumeContent(int jobId);
        public Task<string> ResumeUpload(IFormFile file, int jobId);
    }
}
