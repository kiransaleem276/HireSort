using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
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
        public string File { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileExt { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Compatibility { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string Cnic { get; set; } = null!;
        public bool? IsShortlisted { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual ICollection<Certification> Certifications { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<TechnicalSkill> TechnicalSkills { get; set; }
    }
}
