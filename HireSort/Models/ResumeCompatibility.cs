namespace HireSort.Models
{
    public class ResumeCompatibility
    {
        public int ResumeId { get; set; }
        public string? CandidateName { get; set; }
        public string? Email { get; set; }
        public string? MobieNo { get; set; }
        public string? CompatiblePercentage { get; set; }
        public string? GPA { get; set; }
        public string? InstituteMatch { get; set; }
        public List<CandidateEducation>? Educations { get; set; }
        public List<CandidateExperience>? Experience { get; set; }
        public List<Skills>? Skills { get; set; }
        public List<Links>? Links { get; set; }

    }
    public class CandidateEducation
    {
        public int EduId { get; set; }
        public string? InstituteName { get; set; }
        public string? DegreeName { get; set; }
        public string? CGPA { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

    }
    public class CandidateExperience
    {
        public int ExperienceId { get; set; }
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        public string? Responsiblility { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int TotalExperience { get; set; }
    }
    public class Skills
    {
        public int SkillsId { get; set; }
        public string? SkillName { get; set; }
    }
    public class Links
    {
        public int LinkId { get; set; }
        public string? LinkType { get; set; }
        public string? Link { get; set; }

    }
}
