using HireSort.Context;
using HireSort.Helpers;
using HireSort.Models;
using HireSort.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace HireSort.Repository.Implementation
{
    public class Dashboard : IDashboard
    {
        private int clientId = 1;
        private readonly HRContext _dbContext;
        private readonly string _dateTimeFormat, _dateFormat;

        public Dashboard(HRContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dateTimeFormat = configuration["AppSettings:DateTimeFormat"];
            _dateFormat = configuration["AppSettings:DateFormat"];
        }
        public async Task<ApiResponseMessage> GetDepartment()
        {
            try
            {
                var departmentList = await _dbContext.Departments.Where(w => w.ClientId == clientId && w.IsActive == true).Select(s => new Department()
                {
                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.DepartmentName,
                }).ToListAsync();

                return CommonHelper.GetApiSuccessResponse(departmentList);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }
        public async Task<ApiResponseMessage> GetVacanciesDepartmentWise(int departId)
        {
            try
            {
                var vacancies = await _dbContext.Jobs.Where(w => w.ClientId == clientId && w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now && w.DepartmentId == departId && w.IsActive == true).Select(s => new VacanciesDepartmentWise()
                {
                    VacancyId = s.JobId,
                    VacancyName = s.JobName
                }).ToListAsync();
                return CommonHelper.GetApiSuccessResponse(vacancies);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> GetDepartAndVacacyDetails(int departId = 0, int vacancyId = 0)
        {
            try
            {
                var list = _dbContext.Jobs.Where(w => w.ClientId == clientId && w.IsActive == true && w.Department.IsActive == true).Select(s => new DepartmentAndVacancyList()
                {
                    DepertId = s.DepartmentId,
                    DepartmentName = s.Department.DepartmentName,
                    VacancyId = s.JobId,
                    VacancyName = s.JobName
                });
                if (departId > 0 && vacancyId > 0)
                {
                    list = list.Where(w => w.DepertId == departId && w.VacancyId == vacancyId);
                }
                else if (departId > 0)
                {
                    list = list.Where(w => w.DepertId == departId);
                }
                else if (vacancyId > 0)
                {
                    list = list.Where(w => w.VacancyId == vacancyId);
                }
                return CommonHelper.GetApiSuccessResponse(list);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> GetAllResumes(int departId, int vacancyId, bool isShortListedResume = false)
        {
            try
            {
                var resumeList = new ResumeDetails() { Resumes = new() };
                resumeList.clientId = clientId;
                resumeList.DepartId = departId;
                resumeList.VacancyId = vacancyId;
                if (departId > 0 && vacancyId > 0)
                {
                    var resumes = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == vacancyId && w.Job.DepartmentId == departId).Select(s => new Resumes()
                    {
                        ResumeID = s.Id,
                        JobId = s.JobId,
                        CandidateName = (s.IsFileParsed == true) ? s.FirstName + " " + s.LastName : s.FileName,
                        MobileNo = s.MobileNo,
                        EmailAddress = s.Email,
                        IsShortListed = s.IsShortlisted,
                        ShortListedDate = (s.ShortlistDate != null) ? Convert.ToDateTime(s.ShortlistDate).ToString(_dateFormat) : null,
                        IsFileParsed = s.IsFileParsed,
                        IsCompatibilityCheck = s.IsCompatibility,
                        Compatibility = s.Compatibility
                    });

                    if (isShortListedResume)
                        resumes = resumes.Where(w => w.IsShortListed == true);

                    resumeList.Resumes = resumes.OrderByDescending(o => o.ResumeID).ToList();
                }
                return CommonHelper.GetApiSuccessResponse(resumeList);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> GetDepartmentVacancyCount()
        {
            try
            {
                var response = _dbContext.Departments.Where(w => w.ClientId == clientId && w.IsActive == true).Select(s => new DepartJobCount()
                {
                    DepatId = s.DepartmentId,
                    DepartmentName = s.DepartmentName,
                    VacancyCounts = s.Jobs.Where(a => a.IsActive == true).Count()
                });
                return CommonHelper.GetApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> GetDepartmentJobs(int departId)
        {
            try
            {
                var response = _dbContext.Jobs.Where(w => w.ClientId == clientId && w.DepartmentId == departId && w.IsActive == true).Select(s => new DepartmentJobList()
                {
                    DepartId = s.DepartmentId,
                    JobId = s.JobId,
                    JobName = s.JobName,
                    JobStartDate = s.StartDate.ToString(_dateFormat),
                    JobEndDate = (s.EndDate != null) ? Convert.ToDateTime(s.EndDate).ToString(_dateFormat) : "Not Available"
                });
                return CommonHelper.GetApiSuccessResponse(response);

            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> GetJobDetail(int departId, int jobId)
        {
            try
            {
                var response = _dbContext.Jobs.Where(w => w.ClientId == clientId && w.DepartmentId == departId && w.JobId == jobId).Select(s => new JobDetails
                {
                    JobId = s.JobId,
                    JobName = s.JobName,
                    JobStartDate = s.StartDate.ToString(_dateFormat),
                    JobEndDate = (s.EndDate != null) ? Convert.ToDateTime(s.EndDate).ToString(_dateFormat) : null,
                    JobShift = s.JobDetails.FirstOrDefault(a => a.JobCode.CodeName == "Job Shift").Description ?? null,
                    JobType = s.JobDetails.FirstOrDefault(b => b.JobCode.CodeName == "Job Type").Description ?? null,
                    JobDesc = s.JobDetails.Where(w => w.JobCode.CodeName != "Job Shift" && w.JobCode.CodeName != "Job Type").Select(s => new JobDescription
                    {
                        JobDetailId = s.Id,
                        JobCode = s.JobCode.CodeName,
                        Description = s.Description,
                    }).ToList()
                });
                return CommonHelper.GetApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> GetResumeCompatibiltiy(int resumeId, int jobId)
        {
            try
            {
                var response = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.JobId == jobId && w.Id == resumeId && w.IsCompatibility == true).Select(s => new ResumeCompatibility()
                {
                    ResumeId = s.Id,
                    CandidateName = s.FileName + " " + s.LastName,
                    MobieNo = s.MobileNo,
                    Email = s.Email,
                    CompatiblePercentage = s.Compatibility,
                    GPA = !String.IsNullOrEmpty(s.Gpa) ? s.Gpa : "Not Available",
                    InstituteMatch = s.InstituteMatch,
                    Educations = s.Educations.Where(w => w.ResumeId == s.Id).Select(a => new CandidateEducation()
                    {
                        EduId = a.Id,
                        DegreeName = a.DegreeName,
                        InstituteName = a.InstituteName,
                        CGPA = a.Cgpa,
                        StartDate = (a.StartDate != null) ? Convert.ToDateTime(a.StartDate).ToString(_dateFormat) : null,
                        EndDate = (a.EndDate != null) ? Convert.ToDateTime(a.EndDate).ToString(_dateFormat) : null
                    }).ToList(),
                    Experience = s.Experiences.Where(w => w.ResumeId == s.Id).Select(b => new CandidateExperience()
                    {
                        ExperienceId = b.Id,
                        CompanyName = b.CompanyName,
                        Designation = b.Designation,
                        Responsiblility = b.Responsibility,
                        StartDate = (b.StartDate != null) ? Convert.ToDateTime(b.StartDate).ToString(_dateFormat) : null,
                        EndDate = (b.EndDate != null) ? Convert.ToDateTime(b.EndDate).ToString(_dateFormat) : null,
                        TotalExperience = b.TotalExperience
                    }).ToList(),
                    Skills = s.TechnicalSkills.Where(w => w.ResumeId == s.Id).Select(c => new Skills()
                    {
                        SkillsId = c.Id,
                        SkillName = c.Skills
                    }).ToList(),
                    Links = s.Links.Where(w => w.ResumeId == s.Id).Select(d => new Links()
                    {
                        LinkId = d.Id,
                        LinkType = d.LintType,
                        Link = d.Links
                    }).ToList()
                }); ;
                return CommonHelper.GetApiSuccessResponse(response);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> ResumeShorlisting(int resumeId)
        {
            try
            {
                var resume = _dbContext.Resumes.Where(w => w.ClientId == clientId && w.Id == resumeId && w.IsCompatibility == true && w.IsShortlisted != true).FirstOrDefault();
                if (resume != null)
                {
                    resume.IsShortlisted = true;
                    resume.ShortlistDate = DateTime.Now;
                    _dbContext.Entry(resume).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return CommonHelper.GetApiSuccessResponse("Successfully Shorlisted.");
                }
                return CommonHelper.GetApiSuccessResponse("Resume Not Found.", 400);
            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }

        public async Task<ApiResponseMessage> Login(string email, string password)
        {
            try
            {
                var login = await _dbContext.Logins.Where(w => w.Email == email).FirstOrDefaultAsync();
                if (login != null)
                {
                    return CommonHelper.GetApiSuccessResponse("Email Not Found", 400);
                }
                else if (login?.Password != password)
                {
                    return CommonHelper.GetApiSuccessResponse("Password Doesn't Match", 400);
                }
                else if (login.Password == password && login.IsActive == false)
                {
                    return CommonHelper.GetApiSuccessResponse("Account is not active", 400);
                }
                else if (login.Password == password && login.IsActive == true)
                {
                    return CommonHelper.GetApiSuccessResponse("Login Successfully");
                }
                return CommonHelper.GetApiSuccessResponse("Login Unsuccessfully", 400);

            }
            catch (Exception ex)
            {
                string exceptionString = ex.Message + ex.StackTrace + (ex.InnerException != null ? ex.InnerException.ToString() : "");
                return CommonHelper.GetApiSuccessResponse(exceptionString, 400);
            }
        }
    }
}
