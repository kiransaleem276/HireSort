using HireSort.Models;
using HireSort.Repository.Interface;
using System.Data;
using System.Net;

namespace HireSort.Repository.Implementation
{
    public class Dashboard : IDashboard
    {
        public async Task<ApiResponseMessage> GetDepartment(int clientId)
        {
            List<Department> departmentList = new List<Department>();
            departmentList.Add(new Department()
            {
                DepartmentId = 1,
                DepartmentName = "Information Technology"
            });

            departmentList.Add(new Department()
            {
                DepartmentId = 2,
                DepartmentName = "Human Resource"
            });

            departmentList.Add(new Department()
            {
                DepartmentId = 3,
                DepartmentName = "Finance"
            });
            return GetApiSuccessResponse(departmentList);

            //return dt;

        }

        public static ApiResponseMessage GetApiSuccessResponse(dynamic successData, int statusCode = 0, string message = null)
        {
            return new ApiResponseMessage()
            {
                StatusCode = statusCode == 0 ? Convert.ToInt32(HttpStatusCode.OK) : statusCode,
                Date = Convert.ToString(DateTime.Now),
                Message = message,
                SuccessData = successData,
                ErrorData = null
            };
        }

    }
}
