using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleDataService.DAL
{
    public partial class DairyContext : DbContext
    {
        public DairyContext()
        {
        }

        public DairyContext(DbContextOptions<DairyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Auditorium> Auditorium { get; set; }
        public virtual DbSet<ContactInfo> ContactInfo { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupMessage> GroupMessage { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<LessonInfo> LessonInfo { get; set; }
        public virtual DbSet<Licenses> Licenses { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<TeacherSkill> TeacherSkill { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SQL6007.site4now.net;Initial Catalog=DB_A4BE9F_AndyBor;User Id=DB_A4BE9F_AndyBor_admin;Password=andrybor011599;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId).HasColumnName("account_type_id");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(150);

                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.AccountTypeId)
                    .HasConstraintName("FK__account__account__4316F928");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("account_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.ToTable("auditorium");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.ToTable("contact_info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(150);

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountOfHours).HasColumnName("amount_of_hours");

                entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__course__speciali__3B75D760");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__group__course_id__46E78A0C");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__group__specializ__45F365D3");
            });

            modelBuilder.Entity<GroupMessage>(entity =>
            {
                entity.ToTable("group_message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.File).HasColumnName("file");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMessage)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__group_mes__group__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMessage)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__group_mes__userI__6B24EA82");
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.ToTable("homework");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");

                entity.Property(e => e.Task).HasColumnName("task");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FK__homework__schedu__656C112C");
            });

            modelBuilder.Entity<LessonInfo>(entity =>
            {
                entity.ToTable("lesson_info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.IsPresent).HasColumnName("isPresent");

                entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.LessonInfo)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FK__lesson_in__sched__5FB337D6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LessonInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__lesson_in__userI__60A75C0F");
            });

            modelBuilder.Entity<Licenses>(entity =>
            {
                entity.ToTable("licenses");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsAssigned).HasColumnName("isAssigned");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("material");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Reference).HasColumnName("reference");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId).HasColumnName("account_type_id");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AccountTypeId)
                    .HasConstraintName("FK__news__account_ty__5AEE82B9");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuditoriumId).HasColumnName("auditorium_id");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.Property(e => e.Theme)
                    .HasColumnName("theme")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.AuditoriumId)
                    .HasConstraintName("FK__schedule__audito__5812160E");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__schedule__group___5629CD9C");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__schedule__subjec__5535A963");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__schedule__teache__571DF1D5");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("specialization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__student__group_i__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__student__user_id__4E88ABD4");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__subject__course___3E52440B");
            });

            modelBuilder.Entity<TeacherSkill>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.SubjectId });

                entity.ToTable("teacher_skill");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TeacherSkill)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__teacher_s__subje__5165187F");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherSkill)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__teacher_s__teach__52593CB8");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.AccountId)
                    .HasName("UQ__user__46A222CCEEA5B9F6")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatingDay)
                    .HasColumnName("creating_day")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(150);

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.AccountId)
                    .HasConstraintName("FK__user__account_id__4AB81AF0");
            });
        }
    }
}
