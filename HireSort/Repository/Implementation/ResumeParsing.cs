using HireSort.Context;
using HireSort.Entity.DbModels;
using HireSort.Helpers;
using HireSort.Repository.Interface;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> ResumeUpload(IFormFile file, int jobId)
        {
            try
            {
                string uploads = Path.Combine("C:/Users/HUB360/Desktop/Masters/Practicum/HireSort/HireSort/Resumes/");

                var resume = new Resume();
                resume.JobId = jobId;
                resume.FileExt = System.IO.Path.GetExtension(file.FileName);
                resume.ClientId = clientId;
                _dbContext.Resumes.Add(resume);
                _dbContext.SaveChanges();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + "_" + resume.Id;
                string filePath = Path.Combine(uploads, fileName + resume.FileExt);

                resume.FileName = fileName;
                resume.File = filePath;

                _dbContext.Update(resume).State = EntityState.Modified;
                _dbContext.SaveChanges();

                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }

                return "Success.";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public async Task<string> resumeCheckCompatibility(int resumeId, int jobId)
        {
            try
            {

                var resume = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == jobId && w.Id == resumeId && w.IsFileParsed != true).FirstOrDefault();
                if (resume != null)
                {
                    var clientHighlights = _dbContext.ClientHighlights.Where(w => w.ClientId == clientId && w.IsActive == true).FirstOrDefault();
                    ParseResumeResponse parseResponse = await ResumeParsingHelper.ResumeParse(resume.File);
                    if (parseResponse.Info != null && parseResponse.Value != null && parseResponse != null)
                    {
                        resume.FirstName = parseResponse.EasyAccess().GetCandidateName()?.GivenName + " " + parseResponse.EasyAccess().GetCandidateName()?.MiddleName;
                        resume.LastName = parseResponse.EasyAccess().GetCandidateName()?.FamilyName ?? "";
                        resume.Email = parseResponse.EasyAccess().GetEmailAddresses()?.FirstOrDefault() ?? "";
                        resume.MobileNo = parseResponse.EasyAccess().GetPhoneNumbers()?.FirstOrDefault() ?? "";
                        resume.Cnic = parseResponse.EasyAccess().GetNationalIdentities()?.Select(s => s.Number).FirstOrDefault() ?? "";
                        resume.IsFileParsed = true;


                        double Compatibility = 0;
                        int percentage = 100;
                        var workHistory = new List<Experience>();
                        var educations = new List<Education>();
                        if (parseResponse.Value.ResumeData.Education.EducationDetails.Count > 0)
                        {
                            foreach (var edu in parseResponse.Value.ResumeData.Education.EducationDetails)
                            {
                                educations.Add(new Education()
                                {
                                    ResumeId = resumeId,
                                    InstituteName = edu.SchoolName?.Normalized ?? "",
                                    DegreeName = edu.Degree?.Name?.Normalized ?? "",
                                    Cgpa = edu.GPA?.Score.ToString() ?? "",
                                    StartDate = (edu.LastEducationDate != null) ? edu.LastEducationDate.Date : null,
                                    EndDate = (edu.LastEducationDate != null) ? edu.LastEducationDate.Date : null,
                                    CreatedOn = DateTime.Now
                                });
                                if (clientHighlights != null && clientHighlights.Category == "gpa" && edu.Degree?.Type == "bachelors" && !String.IsNullOrEmpty(edu.GPA?.Score.ToString()))
                                {
                                    Compatibility += (Convert.ToDouble(edu.GPA?.Score) / Convert.ToDouble(clientHighlights.Description)) * 60;
                                    percentage -= 60;
                                }

                            }

                            _dbContext.Educations.AddRange(educations);
                            _dbContext.SaveChanges();
                        }
                        if (parseResponse.Value.ResumeData.EmploymentHistory.Positions.Count > 0)
                        {
                            foreach (var job in parseResponse.Value.ResumeData.EmploymentHistory.Positions)
                            {
                                //if(job.StartDate!=null || job.EndDate != null)
                                //years  
                                int Years = job.EndDate.Date.Year - job.StartDate.Date.Year;
                                int month = job.EndDate.Date.Month - job.StartDate.Date.Month;
                                int TotalMonths = (Years * 12) + month;

                                workHistory.Add(new Experience()
                                {
                                    ResumeId = resumeId,
                                    CompanyName = job.Employer?.Name?.Normalized ?? "",
                                    Responsibility = job.Description,
                                    Designation = job.JobTitle?.Normalized ?? "",
                                    TotalExperience = TotalMonths,
                                    StartDate = job.StartDate.Date,
                                    EndDate = job.EndDate.Date,
                                    CreatedOn = DateTime.Now
                                });

                            }
                            int totalExp = workHistory.Sum(s => s.TotalExperience);
                            Compatibility += (totalExp / 40) * percentage;
                            _dbContext.Experiences.AddRange(workHistory);
                            _dbContext.SaveChanges();
                        }
                        resume.Compatibility = Math.Round(Compatibility, 2).ToString();
                        resume.IsCompatibility = true;
                        _dbContext.Update(resume).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                    return "Success.";
                }
                return "File Not Found.";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //public async Task<string> resumeGetCompatibility(int resumeId, int jobId)
        //{

        //    try
        //    {
        //        var resumeCompatibility = new 
        //        var resume = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == jobId && w.Id == resumeId && w.IsFileParsed == true).FirstOrDefault();
        //        if (resume != null)
        //        {

        //            return "Success.";
        //        }
        //        return "File Not Found.";

        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }

}
