namespace HireSort.Models
{
    public class JobDetails
    {
        public int JobId { get; set; }
        public string? JobName { get; set; }
        public string? JobStartDate { get; set; }
        public string? JobEndDate { get; set; }
        public string? JobType { get; set; }
        public string? JobShift { get; set; }
        public List<JobDescription>? JobDesc { get; set; }

    }
    public class JobDescription
    {
        public int JobDetailId { get; set; }
        public string? JobCode { get; set; }
        public string? Description { get; set; }
    }
}
