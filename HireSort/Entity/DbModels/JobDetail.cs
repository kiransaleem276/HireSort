using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class JobDetail
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int JobId { get; set; }
        public int JobCodeId { get; set; }
        public string Description { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual JobCode JobCode { get; set; } = null!;
    }
}
