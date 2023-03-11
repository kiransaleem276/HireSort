using HireSort.Models;
using System.Data;

namespace HireSort.Repository.Interface
{
    public interface IDashboard
    {
        Task<ApiResponseMessage> GetDepartment();
        Task<ApiResponseMessage> GetVacanciesDepartmentWise(int departId);
        Task<ApiResponseMessage> GetDepartAndVacacyDetails(int departId = 0, int vacancyId = 0);
        Task<ApiResponseMessage> GetAllResumes(int departId, int vacancyId, bool isShortListedResume = false);
        Task<ApiResponseMessage> GetDepartmentVacancyCount();
        Task<ApiResponseMessage> GetDepartmentJobs(int departId);
        Task<ApiResponseMessage> GetJobDetail(int departId, int jobId);
        Task<ApiResponseMessage> GetResumeCompatibiltiy(int resumeId, int jobId);
        Task<ApiResponseMessage> ResumeShorlisting(int resumeId);
        Task<ApiResponseMessage> Login(string email, string password);
    }
}
