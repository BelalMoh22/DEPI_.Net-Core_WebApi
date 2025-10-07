using Day21WebApiLab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day21WebApiLab.Data
{
    public class AppDbContext : DbContext 
    {
        #region CTOR'S
        public AppDbContext() {}
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options){}
        #endregion

        #region Property
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> employees { get; set; }
        #endregion

        #region Methods
        // Keep empty; configuration is supplied through DI in Program.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "HR" },
                new Department { DepartmentId = 2, Name = "Developer" }
            );
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}