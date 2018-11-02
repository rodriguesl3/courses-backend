using Chama.Courses.Domain.Entities;
using Chama.Courses.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Courses.Persistence.Context
{
    public class EfDbContext :DbContext
    {

        public EfDbContext(DbContextOptions<EfDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new TeachersConfig());
            modelBuilder.ApplyConfiguration(new StudentsConfig());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Students> Students { get; set; }
    }

    


    

}
