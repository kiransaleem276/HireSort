using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Certification
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string CerificateName { get; set; } = null!;
        public string? InstituteName { get; set; }
        public DateTime? ExpirtyDate { get; set; }

        public virtual Resume Resume { get; set; } = null!;
    }
}
