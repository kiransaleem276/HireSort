using System;
using System.Collections.Generic;

namespace HireSort.Entity.DbModels
{
    public partial class JobCode
    {
        public JobCode()
        {
            JobDetails = new HashSet<JobDetail>();
        }

        public int JobCodeId { get; set; }
        public int ClientId { get; set; }
        public string CodeName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual ICollection<JobDetail> JobDetails { get; set; }
    }
}
