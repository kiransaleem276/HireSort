namespace HireSort.Repository.Interface
{
    public interface IResumeParsing
    {
        public Task<string> resumeContent(string resumeName);
    }
}
