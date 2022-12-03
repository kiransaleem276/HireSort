﻿using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Education
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string InstituteName { get; set; } = null!;
        public string DegreeName { get; set; } = null!;
        public string Cgpa { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Resume Resume { get; set; } = null!;
    }
}