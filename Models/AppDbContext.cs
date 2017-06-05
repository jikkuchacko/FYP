using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FYP.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<AlLecturer> AlLecturer { get; set; }
        public virtual DbSet<ExamVenue> ExamVenue { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<OrgCoordinator> OrgCoordinator { get; set; }
        public virtual DbSet<SaCoordinator> SaCoordinator { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<Timeslot> Timeslot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=FYP;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlLecturer>(entity =>
            {
                entity.ToTable("Al_lecturer");

                entity.HasIndex(e => e.Email)
                    .HasName("AK_Al_lecturer_Email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<ExamVenue>(entity =>
            {
                entity.HasKey(e => e.ClassName)
                    .HasName("PK__exam_ven__7DC4C39C3B8981D2");

                entity.ToTable("exam_venue");

                entity.Property(e => e.ClassName)
                    .HasColumnName("class_name")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.ClassLevel)
                    .IsRequired()
                    .HasColumnName("class_level")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.ModuleCode)
                    .HasName("PK__tmp_ms_x__E5DB09FBE8A551DF");

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("module_code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.ModuleName)
                    .HasColumnName("module_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NumberOfStaff).HasColumnName("number_of_staff");

                entity.Property(e => e.NumberOfStrudents).HasColumnName("number_of_strudents");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<OrgCoordinator>(entity =>
            {
                entity.HasKey(e => e.OrgId)
                    .HasName("PK__ORG_coor__666E16845F48DCA3");

                entity.ToTable("ORG_coordinator");

                entity.Property(e => e.OrgId)
                    .HasColumnName("ORG_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrgContactNo)
                    .IsRequired()
                    .HasColumnName("ORG_contact_no")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.OrgEmail)
                    .IsRequired()
                    .HasColumnName("ORG_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.OrgName)
                    .IsRequired()
                    .HasColumnName("ORG_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<SaCoordinator>(entity =>
            {
                entity.HasKey(e => e.SaId)
                    .HasName("PK__SA_coord__6939A750B3ECE01B");

                entity.ToTable("SA_coordinator");

                entity.Property(e => e.SaId)
                    .HasColumnName("SA_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.SaContactNo)
                    .IsRequired()
                    .HasColumnName("SA_contact_no")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SaEmail)
                    .IsRequired()
                    .HasColumnName("SA_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SaName)
                    .IsRequired()
                    .HasColumnName("SA_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("school");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasColumnName("school_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Timeslot>(entity =>
            {
                entity.HasKey(e => e.ModuleCode)
                    .HasName("PK__tmp_ms_x__E5DB09FB5763FC07");

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("module_code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.AlId).HasColumnName("al_id ");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModuleSchool)
                    .HasColumnName("module_school")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.MsaVenue)
                    .HasColumnName("msa_venue")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");
            });
        }
    }
}