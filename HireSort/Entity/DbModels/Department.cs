using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Department
    {
        public Department()
        {
            Jobs = new HashSet<Job>();
        }

        public int DepartmentId { get; set; }
        public int ClientId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
