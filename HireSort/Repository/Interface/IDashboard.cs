using HireSort.Models;
using System.Data;

namespace HireSort.Repository.Interface
{
    public interface IDashboard
    {
        Task<ApiResponseMessage> GetDepartment();
        Task<ApiResponseMessage> GetVacanciesDepartmentWise(int departId);
        Task<ApiResponseMessage> GetDepartAndVacacyDetails(int departId = 0, int vacancyId = 0);
    }
}
