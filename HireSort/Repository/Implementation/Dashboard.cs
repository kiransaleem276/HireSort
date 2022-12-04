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

                if (departId > 0)
                {
                    list = list.Where(w => w.DepertId == departId);
                }
                if (vacancyId > 0)
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
                        CandidateName = s.FirstName + " " + s.LastName,
                        MobileNo = s.MobileNo,
                        EmailAddress = s.Email,
                        IsShortListed = s.IsShortlisted,
                        ShortListedDate = (s.ShortlistDate != null) ? Convert.ToDateTime(s.ShortlistDate).ToString(_dateFormat) : null
                    });

                    if (isShortListedResume)
                        resumes = resumes.Where(w => w.IsShortListed == true);

                    resumeList.Resumes = resumes.ToList();
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
    }
}
