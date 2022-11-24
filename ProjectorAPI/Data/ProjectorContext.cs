using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectorAPI.models;

namespace ProjectorAPI.Data
{
    public class ProjectorContext : DbContext
    {
        
        public ProjectorContext(DbContextOptions<ProjectorContext> options) : base(options)
        {
        }

        public DbSet<Project> projects { get; set; }
        public DbSet<User> users { get; set; }

        public DbSet<Student> students { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<CourseAssignment> courseAssignments { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<OfficeAssignment> officeAssignments { get; set; }
        public DbSet<Department> departments { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(_config.GetConnectionString("server"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(o => new { o.CourseID,o.InstructorID});
            modelBuilder.Entity<OfficeAssignment>()
                .HasKey(o=> new {o.InstructorID});

            //modelBuilder.Entity<Course>()
            //    .HasOne(o => o.Department);
            //modelBuilder.Entity<Department>()
            //    .HasOne(o => o.Instructor);



        }
    }
}
