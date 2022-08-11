using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EducationalPlatform.Models;

namespace EducationalPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EducationalPlatform.Models.Course>? Course { get; set; }
        public DbSet<EducationalPlatform.Models.Department>? Department { get; set; }
        public DbSet<EducationalPlatform.Models.Student>? Student { get; set; }
        public DbSet<EducationalPlatform.Models.Teacher>? Teacher { get; set; }
        public DbSet<EducationalPlatform.Models.StudentCourse>? StudentCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(bc => new { bc.StudentId, bc.CourseId });
            modelBuilder.Entity<StudentCourse>()
                .HasOne(bc => bc.Student)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(bc => bc.StudentId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(bc => bc.CourseId);
            base.OnModelCreating(modelBuilder);
        }
    }
}