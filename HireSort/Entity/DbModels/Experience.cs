using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Experience
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Responsibility { get; set; }
        public string Designation { get; set; } = null!;
        public int TotalExperience { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Resume Resume { get; set; } = null!;
    }
}
