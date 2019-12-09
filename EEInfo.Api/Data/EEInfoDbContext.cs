using EEInfo.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EEInfo.Api.Data
{
    public class EEInfoDbContext : DbContext
    {
        public EEInfoDbContext(DbContextOptions<EEInfoDbContext> options)
            : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).HasMaxLength(50);

            modelBuilder.Entity<EmployeeTask>()
                .Property(x => x.TaskName).HasMaxLength(100);

            modelBuilder.Entity<EmployeeTask>()
                .HasOne(navigationExpression: x => x.Employee)
                .WithMany(navigationExpression: x => x.EmployeeTasks)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
