using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Client
    {
        public Client()
        {
            AbountUs = new HashSet<AbountU>();
            ClientHighlights = new HashSet<ClientHighlight>();
            ContactUs = new HashSet<ContactU>();
            Departments = new HashSet<Department>();
            Homes = new HashSet<Home>();
            JobCodes = new HashSet<JobCode>();
            JobDetails = new HashSet<JobDetail>();
            Jobs = new HashSet<Job>();
            Logins = new HashSet<Login>();
            Resumes = new HashSet<Resume>();
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<AbountU> AbountUs { get; set; }
        public virtual ICollection<ClientHighlight> ClientHighlights { get; set; }
        public virtual ICollection<ContactU> ContactUs { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
        public virtual ICollection<JobCode> JobCodes { get; set; }
        public virtual ICollection<JobDetail> JobDetails { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
