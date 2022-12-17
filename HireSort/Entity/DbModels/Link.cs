using System;
using System.Collections.Generic;

namespace HireSort.Entity.DbModels
{
    public partial class Link
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string LintType { get; set; } = null!;
        public string Links { get; set; } = null!;

        public virtual Resume Resume { get; set; } = null!;
    }
}
