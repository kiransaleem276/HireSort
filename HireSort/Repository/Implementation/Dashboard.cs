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
        private readonly HRContext _dbContext;
        public Dashboard(HRContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponseMessage> GetDepartment()
        {
            try
            {
                var departmentList = await _dbContext.Departments.Where(w => w.ClientId == 1 && w.IsActive == true).Select(s => new Department()
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
                var vacancies = await _dbContext.Jobs.Where(w => w.ClientId == 1 && w.DepartmentId == departId && w.IsActive == true).Select(s => new VacanciesDepartmentWise()
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
                var list = _dbContext.Jobs.Where(w => w.ClientId == 1 && w.IsActive == true && w.Department.IsActive == true).Select(s => new DepartmentAndVacancyList()
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
    }
}
