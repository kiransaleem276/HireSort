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
        public async Task<ApiResponseMessage> GetDepartment(int clientId)
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
        public async Task<ApiResponseMessage> GetVacanciesDepartmentWise(int clientId, int departId)
        {
            try
            {
                var vacancies = await _dbContext.Jobs.Where(w => w.ClientId == clientId && w.DepartmentId == departId && w.IsActive == true).Select(s => new VacanciesDepartmentWise()
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

        public async Task<ApiResponseMessage> GetDepartAndVacacyDetails(int clientId)
        {
            try
            {
                var list = await _dbContext.Jobs.Where(w => w.ClientId == clientId && w.IsActive == true && w.Department.IsActive == true).Select(s => new DepartmentAndVacancyList()
                {
                    DepertId = s.DepartmentId,
                    DepartmentName = s.Department.DepartmentName,
                    VacancyId = s.JobId,
                    VacancyName = s.JobName
                }).ToListAsync();

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
