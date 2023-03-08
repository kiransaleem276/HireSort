using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HireSort.Entity.DbModels;

namespace HireSort.Context
{
    public partial class HRContext : DbContext
    {
        public HRContext()
        {
        }

        public HRContext(DbContextOptions<HRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbountU> AbountUs { get; set; } = null!;
        public virtual DbSet<Certification> Certifications { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientHighlight> ClientHighlights { get; set; } = null!;
        public virtual DbSet<ContactU> ContactUs { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Education> Educations { get; set; } = null!;
        public virtual DbSet<Experience> Experiences { get; set; } = null!;
        public virtual DbSet<Home> Homes { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobCode> JobCodes { get; set; } = null!;
        public virtual DbSet<JobDetail> JobDetails { get; set; } = null!;
        public virtual DbSet<Link> Links { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Resume> Resumes { get; set; } = null!;
        public virtual DbSet<TechnicalSkill> TechnicalSkills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HUB360-KIRAN-MUHAMMAD-SALEEM;Database=HireSort;Integrated Security=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbountU>(entity =>
            {
                entity.ToTable("ABOUNT_US");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AbountUs)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ABOUNT_US__CLIEN__2A4B4B5E");
            });

            modelBuilder.Entity<Certification>(entity =>
            {
                entity.ToTable("CERTIFICATION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CerificateName)
                    .HasMaxLength(100)
                    .HasColumnName("CERIFICATE_NAME");

                entity.Property(e => e.ExpirtyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EXPIRTY_DATE");

                entity.Property(e => e.InstituteName)
                    .HasMaxLength(100)
                    .HasColumnName("INSTITUTE_NAME");

                entity.Property(e => e.ResumeId).HasColumnName("RESUME_ID");

                entity.HasOne(d => d.Resume)
                    .WithMany(p => p.Certifications)
                    .HasForeignKey(d => d.ResumeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CERTIFICA__RESUM__534D60F1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("CLIENT");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(50)
                    .HasColumnName("CLIENT_NAME");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            });

            modelBuilder.Entity<ClientHighlight>(entity =>
            {
                entity.ToTable("CLIENT_HIGHLIGHTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientHighlights)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CLIENT_HI__CLIEN__32E0915F");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.ToTable("CONTACT_US");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ContactUs)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CONTACT_U__CLIEN__2D27B809");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .HasColumnName("DEPARTMENT_NAME");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DEPARTMEN__CLIEN__35BCFE0A");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("EDUCATION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cgpa)
                    .HasMaxLength(20)
                    .HasColumnName("CGPA");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DegreeName)
                    .HasMaxLength(100)
                    .HasColumnName("DEGREE_NAME");

                entity.Property(e => e.DegreeType).HasColumnName("DEGREE_TYPE");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.InstituteName)
                    .HasMaxLength(100)
                    .HasColumnName("INSTITUTE_NAME");

                entity.Property(e => e.ResumeId).HasColumnName("RESUME_ID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.Resume)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.ResumeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EDUCATION__RESUM__47DBAE45");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.ToTable("EXPERIENCE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyName).HasColumnName("COMPANY_NAME");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Designation).HasColumnName("DESIGNATION");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.Responsibility).HasColumnName("RESPONSIBILITY");

                entity.Property(e => e.ResumeId).HasColumnName("RESUME_ID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("START_DATE");

                entity.Property(e => e.TotalExperience).HasColumnName("TOTAL_EXPERIENCE");

                entity.HasOne(d => d.Resume)
                    .WithMany(p => p.Experiences)
                    .HasForeignKey(d => d.ResumeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EXPERIENC__RESUM__74AE54BC");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("TYPE");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HOME__CLIENT_ID__300424B4");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("JOBS");

                entity.Property(e => e.JobId).HasColumnName("JOB_ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.JobName)
                    .HasMaxLength(50)
                    .HasColumnName("JOB_NAME");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("START_DATE");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOBS__CLIENT_ID__38996AB5");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOBS__DEPARTMENT__398D8EEE");
            });

            modelBuilder.Entity<JobCode>(entity =>
            {
                entity.ToTable("JOB_CODE");

                entity.Property(e => e.JobCodeId).HasColumnName("JOB_CODE_ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(50)
                    .HasColumnName("CODE_NAME");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.JobCodes)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOB_CODE__CLIENT__3C69FB99");
            });

            modelBuilder.Entity<JobDetail>(entity =>
            {
                entity.ToTable("JOB_DETAIL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

                entity.Property(e => e.JobCodeId).HasColumnName("JOB_CODE_ID");

                entity.Property(e => e.JobId).HasColumnName("JOB__ID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.JobDetails)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOB_DETAI__CLIEN__6FE99F9F");

                entity.HasOne(d => d.JobCode)
                    .WithMany(p => p.JobDetails)
                    .HasForeignKey(d => d.JobCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOB_DETAI__JOB_C__71D1E811");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobDetails)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOB_DETAI__JOB____70DDC3D8");
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.ToTable("LINKS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Links)
                    .HasMaxLength(100)
                    .HasColumnName("LINKS");

                entity.Property(e => e.LintType)
                    .HasMaxLength(50)
                    .HasColumnName("LINT_TYPE");

                entity.Property(e => e.ResumeId).HasColumnName("RESUME_ID");

                entity.HasOne(d => d.Resume)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.ResumeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LINKS__RESUME_ID__5070F446");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CreatedOn)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("USER_NAME");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LOGIN__CLIENT_ID__276EDEB3");
            });

            modelBuilder.Entity<Resume>(entity =>
            {
                entity.ToTable("RESUME");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.Cnic)
                    .HasMaxLength(100)
                    .HasColumnName("CNIC");

                entity.Property(e => e.Compatibility)
                    .HasMaxLength(50)
                    .HasColumnName("COMPATIBILITY");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.File).HasColumnName("FILE");

                entity.Property(e => e.FileExt)
                    .HasMaxLength(20)
                    .HasColumnName("FILE_EXT");

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .HasColumnName("FILE_NAME");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.IsCompatibility).HasColumnName("IS_COMPATIBILITY");

                entity.Property(e => e.IsShortlisted).HasColumnName("IS_SHORTLISTED");

                entity.Property(e => e.JobId).HasColumnName("JOB__ID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(100)
                    .HasColumnName("MOBILE_NO");

                entity.Property(e => e.ShortlistDate)
                    .HasColumnType("datetime")
                    .HasColumnName("SHORTLIST_DATE");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Resumes)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RESUME__CLIENT_I__5BE2A6F2");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Resumes)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RESUME__JOB__ID__5CD6CB2B");
            });

            modelBuilder.Entity<TechnicalSkill>(entity =>
            {
                entity.ToTable("TECHNICAL_SKILLS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Duration).HasColumnName("DURATION");

                entity.Property(e => e.ResumeId).HasColumnName("RESUME_ID");

                entity.Property(e => e.Skills)
                    .HasMaxLength(100)
                    .HasColumnName("SKILLS");

                entity.HasOne(d => d.Resume)
                    .WithMany(p => p.TechnicalSkills)
                    .HasForeignKey(d => d.ResumeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TECHNICAL__RESUM__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
