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

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(_config.GetConnectionString("server"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Project>().ToTable("Project");
        }
    }
}
