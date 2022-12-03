using HireSort.Models;
using System.Net;

namespace HireSort.Helpers
{
    public static class CommonHelper
    {
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
