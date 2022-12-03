using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Job
    {
        public Job()
        {
            JobDetails = new HashSet<JobDetail>();
            Resumes = new HashSet<Resume>();
        }

        public int JobId { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string JobName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<JobDetail> JobDetails { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
