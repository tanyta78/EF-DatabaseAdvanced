namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }
        
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=DESKTOP-LAHCAG9\SQLEXPRESS;Database=StudentSystem;Integrated Security=True;");
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>(s =>
            {
                s.Property(p => p.Name).HasMaxLength(100).IsUnicode();
                s.Property(p => p.PhoneNumber).HasColumnType("char(10)").IsUnicode(false).IsRequired(false);
                s.Property(p => p.Birthday).IsRequired(false);
            });

            builder.Entity<Course>(c =>
            {

                c.Property(p => p.Name).HasMaxLength(80).IsUnicode();
                c.Property(p => p.Description).IsUnicode().IsRequired(false);

            });

            builder.Entity<Resource>(r =>
            {
                r.Property(p => p.Name).HasMaxLength(50).IsUnicode();
                r.Property(p => p.Url).IsUnicode(false);
            });

            builder.Entity<Homework>(h =>
            {
                h.Property(p => p.Content).IsUnicode(false);
            });

            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            //student
            builder.Entity<Student>()
                .HasMany(s => s.CourseEnrollments)
                .WithOne(ce => ce.Student)
                .HasForeignKey(ce => ce.StudentId);

            builder.Entity<Student>()
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(hs => hs.Student)
                .HasForeignKey(hs => hs.StudentId);


            //course
            builder.Entity<Course>()
                .HasMany(c => c.StudentsEnrolled)
                .WithOne(st => st.Course)
                .HasForeignKey(st => st.CourseId);

            builder.Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            builder.Entity<Course>()
                .HasMany(c => c.HomeworkSubmissions)
                .WithOne(hs => hs.Course)
                .HasForeignKey(hs => hs.CourseId);


            base.OnModelCreating(builder);
        }
    }
}
