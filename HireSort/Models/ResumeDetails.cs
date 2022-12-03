namespace HireSort.Models
{
    public class ResumeDetails
    {
        public int clientId { get; set; }
        public int DepartId { get; set; }
        public int VacancyId { get; set; }

        public List<Resumes>? Resumes { get; set; }
    }
    public class Resumes
    {
        public int ResumeID { get; set; }
        public string? CandidateName { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailAddress { get; set; }
        public bool? IsShortListed { get; set; }
        public string? ShortListedDate { get; set; }
    }
}
