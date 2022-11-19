namespace HireSort.Models
{
    public class ApiResponseMessage
    {
        public string Message { set; get; }
        public string Date { set; get; }
        public int StatusCode { set; get; }
        public dynamic ErrorData { get; set; }
        public dynamic SuccessData { get; set; }
    }

}
