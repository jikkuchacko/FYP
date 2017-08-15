using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FYP.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<AssociateLecturer> AssociateLecturer { get; set; }
        public virtual DbSet<ExamVenue> ExamVenue { get; set; }
        public virtual DbSet<LectSlot> LectSlot { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<OrgCoordinator> OrgCoordinator { get; set; }
        public virtual DbSet<SaCoordinator> SaCoordinator { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<Timeslot> Timeslot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=FYP;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssociateLecturer>(entity =>
            {
                entity.HasKey(e => e.AlId)
                    .HasName("PK__associat__84248F8340011A4D");

                entity.ToTable("associate_lecturer");

                entity.Property(e => e.AlId).HasColumnName("al_id");

                entity.Property(e => e.AlContactNumber)
                    .IsRequired()
                    .HasColumnName("al_contact_number")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.AlEmail)
                    .IsRequired()
                    .HasColumnName("al_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.AlName)
                    .IsRequired()
                    .HasColumnName("al_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.AlSchool)
                    .IsRequired()
                    .HasColumnName("al_school")
                    .HasColumnType("varchar(45)");


                entity.Property(e => e.SchoolSchoolId).HasColumnName("school_school_id");

                entity.HasOne(d => d.SchoolSchool)
                    .WithMany(p => p.AssociateLecturer)
                    .HasForeignKey(d => d.SchoolSchoolId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__associate__schoo__31EC6D26");
            });

            modelBuilder.Entity<ExamVenue>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK__exam_ven__FDF479868DFD9F0D");

                entity.ToTable("exam_venue");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassLevel)
                    .HasColumnName("class_level")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasColumnName("class_name")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.TimeslotAlLecturerAlId).HasColumnName("Timeslot_al_lecturer_al_id");

                entity.Property(e => e.TimeslotModuleCode)
                    .IsRequired()
                    .HasColumnName("Timeslot_module_code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.TimeslotTimeslotId).HasColumnName("Timeslot_timeslot_id");

                //entity.HasOne(d => d.TimeslotTimeslot)
                //    .WithMany(p => p.ExamVenue)
                //    .HasForeignKey(d => d.TimeslotTimeslotId)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasConstraintName("FK__exam_venu__Times__32E0915F");
            });

            modelBuilder.Entity<LectSlot>(entity =>
            {
                entity.HasKey(e => e.PreferredTimeslotId)
                    .HasName("PK__lect_slo__4169FEBAD048C27A");

                entity.ToTable("lect_slot");

                entity.Property(e => e.PreferredTimeslotId).HasColumnName("preferred_timeslot_id");

                entity.Property(e => e.AssociateLecturerAlId).HasColumnName("associate_lecturer_al_id");

                entity.Property(e => e.RequestTime)
                    .HasColumnName("request_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TimeslotTimeslot).HasColumnName("Timeslot_timeslot");

                entity.HasOne(d => d.AssociateLecturerAl)
                    .WithMany(p => p.LectSlot)
                    .HasForeignKey(d => d.AssociateLecturerAlId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__lect_slot__assoc__37A5467C");

                //entity.HasOne(d => d.TimeslotTimeslotNavigation)
                //    .WithMany(p => p.LectSlot)
                //    .HasForeignKey(d => d.TimeslotTimeslot)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasConstraintName("FK__lect_slot__Times__38996AB5");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("module");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("module_code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasColumnName("module_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NumberOfStaff).HasColumnName("number_of_staff");

                entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");

                entity.Property(e => e.SchoolSchoolId).HasColumnName("school_school_id");

                entity.HasOne(d => d.SchoolSchool)
                    .WithMany(p => p.Module)
                    .HasForeignKey(d => d.SchoolSchoolId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__module__school_s__30F848ED");
            });

            modelBuilder.Entity<OrgCoordinator>(entity =>
            {
                entity.HasKey(e => e.OrgId)
                    .HasName("PK__ORG_coor__666E16845E91E730");

                entity.ToTable("ORG_coordinator");

                entity.Property(e => e.OrgId)
                    .HasColumnName("ORG_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrgContactNumber)
                    .HasColumnName("ORG_contact_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.OrgEmail)
                    .HasColumnName("ORG_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.OrgName)
                    .HasColumnName("ORG_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SchoolSchoolId).HasColumnName("school_school_id");

                entity.HasOne(d => d.SchoolSchool)
                    .WithMany(p => p.OrgCoordinator)
                    .HasForeignKey(d => d.SchoolSchoolId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__ORG_coord__schoo__36B12243");
            });

            modelBuilder.Entity<SaCoordinator>(entity =>
            {
                entity.HasKey(e => e.SaId)
                    .HasName("PK__SA_Coord__6939A750FAC9292E");

                entity.ToTable("SA_Coordinator");

                entity.Property(e => e.SaId)
                    .HasColumnName("SA_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.SaContactNumber)
                    .HasColumnName("SA_contact_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SaEmail)
                    .HasColumnName("SA_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SaName)
                    .HasColumnName("SA_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SchoolSchoolId).HasColumnName("school_school_id");

                entity.HasOne(d => d.SchoolSchool)
                    .WithMany(p => p.SaCoordinator)
                    .HasForeignKey(d => d.SchoolSchoolId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__SA_Coordi__schoo__35BCFE0A");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("school");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.SchoolDepartment)
                    .IsRequired()
                    .HasColumnName("school_department")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasColumnName("school_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Timeslot>(entity =>
            {
                entity.Property(e => e.timeslot_id).HasColumnName("timeslot_id");

                entity.Property(e => e.associate_lecturer_al_id).HasColumnName("associate_lecturer_al_id");

                entity.Property(e => e.duration_of_exam).HasColumnName("duration_of_exam");

                entity.Property(e => e.examDate)
                    .HasColumnName("examDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.moduleModuleCode)
                    .IsRequired()
                    .HasColumnName("module_module_code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.module_module_id).HasColumnName("module_module_id");

                entity.Property(e => e.module_schoolcentre)
                    .IsRequired()
                    .HasColumnName("module_schoolcentre")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.start_time)
                    .HasColumnName("start_time")
                    .HasColumnType("date");

                //entity.HasOne(d => d.AssociateLecturerAl)
                //    .WithMany(p => p.Timeslot)
                //    .HasForeignKey(d => d.AssociateLecturerAlId)
                //    .HasConstraintName("FK__Timeslot__associ__33D4B598");

                //entity.HasOne(d => d.ModuleModule)
                //    .WithMany(p => p.Timeslot)
                //    .HasForeignKey(d => d.ModuleModuleId)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasConstraintName("FK__Timeslot__module__34C8D9D1");
            });
        }
    }
}