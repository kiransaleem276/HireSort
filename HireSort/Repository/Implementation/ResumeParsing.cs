using HireSort.Helpers;
using HireSort.Repository.Interface;

namespace HireSort.Repository.Implementation
{
    public class ResumeParsing : IResumeParsing
    {
        public async Task<string> resumeContent(string resumeName)
        {
            try
            {
                return await ResumeParsingHelper.ContactInfo();
                //return resumeName;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
