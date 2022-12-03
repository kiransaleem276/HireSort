using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class TechnicalSkill
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string Skills { get; set; } = null!;
        public int Duration { get; set; }

        public virtual Resume Resume { get; set; } = null!;
    }
}
