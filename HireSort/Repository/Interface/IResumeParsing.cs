namespace HireSort.Repository.Interface
{
    public interface IResumeParsing
    {
        public Task<string> resumeCheckCompatibility(int resumeId, int jobId);
        public Task<string> ResumeUpload(IFormFile file, int jobId);
    }
}
