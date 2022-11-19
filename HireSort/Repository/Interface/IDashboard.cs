using HireSort.Models;
using System.Data;

namespace HireSort.Repository.Interface
{
    public interface IDashboard
    {
        Task<ApiResponseMessage> GetDepartment(int clientId);
    }
}
