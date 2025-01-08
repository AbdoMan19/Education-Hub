using Microsoft.EntityFrameworkCore;
using MVC_Task.DB.Models;

namespace MVC_Task.DB
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=EducationTask;Integrated Security=True;trust server certificate=true;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseResult> CoursesResult { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseResult>(c =>
            {
                c.HasOne(c => c.Trainee).WithMany().HasForeignKey(c => c.Trainee_Id).OnDelete(DeleteBehavior.Cascade);
                c.HasOne(c => c.Course).WithMany().HasForeignKey(c => c.Crs_Id).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Instructor>(i =>
            {
                i.HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

                i.HasOne(i => i.Course)
                .WithMany(c => c.Instructors).HasForeignKey(i => i.CourseId).OnDelete(DeleteBehavior.SetNull);
            });
             

            modelBuilder.Entity<Trainee>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Trainees)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>(c =>
            {
                c.HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

                c.HasIndex(c => c.Name).IsUnique();

            });
                
            modelBuilder.Entity<Department>(d =>
            {
                d.HasIndex(d => d.Name).IsUnique();
            });
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
