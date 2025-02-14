using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Models;

namespace IOCL_Training_Module.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Completed> CompletedTrainings { get; set; }
        public DbSet<NotCompleted> NotCompletedTrainings { get; set; }
        public DbSet<RecurringTraining> RecurringTasks { get; set; }
        public DbSet<EmployeeReporting> Reportings { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Employee Table Primary Key
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmpNo);

            // 🔹 Training Table Primary Key
            modelBuilder.Entity<Training>()
                .HasKey(t => t.TrainingID);

            // 🔹 Completed Training Table Primary Key
            modelBuilder.Entity<Completed>()
                .HasKey(c => c.SrNo);

            modelBuilder.Entity<Completed>()
                .HasOne(c => c.Employee)
                .WithMany()
                .HasForeignKey(c => c.EmpNo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Completed>()
                .HasOne(c => c.Training)
                .WithMany()
                .HasForeignKey(c => c.CompletedTraining)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Not Completed Training Table Primary Key
            modelBuilder.Entity<NotCompleted>()
                .HasKey(nc => nc.SNo);

            modelBuilder.Entity<NotCompleted>()
                .HasOne(nc => nc.Employee)
                .WithMany()
                .HasForeignKey(nc => nc.EmpNo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NotCompleted>()
                .HasOne(nc => nc.Training)
                .WithMany()
                .HasForeignKey(nc => nc.Code)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Recurring Task Table Primary Key
            modelBuilder.Entity<RecurringTraining>()
                .HasKey(rt => rt.SrNo);

            // 🔹 Employee Reporting Table (Self-referencing Employee)
            modelBuilder.Entity<EmployeeReporting>()
                .HasKey(r => r.SrNo);  // ✅ Updated for EmployeeReporting

            modelBuilder.Entity<EmployeeReporting>()
                .HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.EmpNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeReporting>()
                .HasOne(r => r.Manager)
                .WithMany()
                .HasForeignKey(r => r.Reporting)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
