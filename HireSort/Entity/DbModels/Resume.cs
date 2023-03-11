using System;
using System.Collections.Generic;

namespace HireSort.Entity.DbModels
{
    public partial class Resume
    {
        public Resume()
        {
            Certifications = new HashSet<Certification>();
            Educations = new HashSet<Education>();
            Experiences = new HashSet<Experience>();
            Links = new HashSet<Link>();
            TechnicalSkills = new HashSet<TechnicalSkill>();
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public int JobId { get; set; }
        public bool? IsShortlisted { get; set; }
        public DateTime? ShortlistDate { get; set; }
        public string? FileName { get; set; }
        public string? FileExt { get; set; }
        public string? File { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Compatibility { get; set; }
        public string? MobileNo { get; set; }
        public string? Cnic { get; set; }
        public bool? IsFileParsed { get; set; }
        public bool? IsCompatibility { get; set; }
        public string? Gpa { get; set; }
        public string? InstituteMatch { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual ICollection<Certification> Certifications { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<TechnicalSkill> TechnicalSkills { get; set; }
    }
}
