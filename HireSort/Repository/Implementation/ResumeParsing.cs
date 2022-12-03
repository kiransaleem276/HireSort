using HireSort.Context;
using HireSort.Entities.DbModels;
using HireSort.Helpers;
using HireSort.Repository.Interface;
using Sovren;
using Sovren.Models.API.Parsing;

namespace HireSort.Repository.Implementation
{
    public class ResumeParsing : IResumeParsing
    {
        private readonly HRContext _dbContext;
        private int clientId = 1;

        public ResumeParsing(HRContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> resumeContent(int jobId)
        {
            try
            {
                ParseResumeResponse parseResponse = await ResumeParsingHelper.ResumeParse();

                if (parseResponse.Info != null && parseResponse.Value != null && parseResponse != null)
                {
                    var resume = new Resume();
                    resume.JobId = jobId;
                    resume.ClientId = clientId;
                    resume.FirstName = parseResponse.EasyAccess().GetCandidateName()?.GivenName + " " + parseResponse.EasyAccess().GetCandidateName()?.MiddleName;
                    resume.LastName = parseResponse.EasyAccess().GetCandidateName()?.FamilyName;
                    resume.Email = parseResponse.EasyAccess().GetEmailAddresses()?.FirstOrDefault();
                    resume.MobileNo = parseResponse.EasyAccess().GetPhoneNumbers()?.FirstOrDefault();
                    resume.Cnic = parseResponse.EasyAccess().GetNationalIdentities()?.Select(s => s.Number).FirstOrDefault() ?? "";
                    resume.File = "file";
                    resume.FileName = "fileName";
                    resume.FileExt = "ext";
                    resume.Compatibility = "0";
                    _dbContext.Resumes.Add(resume);
                    _dbContext.SaveChanges();
                    int resumeId = resume.Id;

                    var workHistory = new List<Experience>();
                    if (parseResponse.Value.ResumeData.EmploymentHistory.Positions.Count > 0)
                    {
                        foreach (var job in parseResponse.Value.ResumeData.EmploymentHistory.Positions)
                        {
                            //years  
                            int Years = job.EndDate.Date.Year - job.StartDate.Date.Year;
                            int month = job.EndDate.Date.Month - job.EndDate.Date.Month;
                            int TotalMonths = (Years * 12) + month;

                            workHistory.Add(new Experience()
                            {
                                ResumeId = resumeId,
                                CompanyName = job.Employer?.Name?.Normalized,
                                Responsibility = job.Description,
                                Designation = job.JobTitle?.Normalized,
                                TotalExperience = TotalMonths,
                                StartDate = job.StartDate.Date,
                                EndDate = job.EndDate.Date,
                                CreatedOn = DateTime.Now
                            });
                        }
                        _dbContext.Experiences.AddRange(workHistory);
                        _dbContext.SaveChanges();
                    }

                }
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
