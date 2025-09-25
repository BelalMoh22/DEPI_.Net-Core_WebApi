using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18CodeFirstLab.Models
{
    internal class SchoolDBContext : DbContext
    {
        public SchoolDBContext()
        {
        }
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) 
        : base(options) 
        {
        }

        // DB Sets : Tables in the database
        public DbSet<Student> Students { get; set; } //DbSet represents a table in the database

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=Belal-2004;Initial Catalog=Day3WebApiDB;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // here we can use Fluent API to configure the model
            modelBuilder.Entity<Student>(student =>
            {
                student.HasKey(s => s.StudentId); // specify primary key using Fluent API
                student.Property(s => s.Name).IsRequired().HasMaxLength(50); // specify that the property is required and has a maximum length using Fluent API


            });

        }
    }
}
