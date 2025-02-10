using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Models;  // Ensure this matches your model namespace

namespace IOCL_Training_Module.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSets for all tables
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Completed> CompletedTrainings { get; set; }
        public DbSet<NotCompleted> NotCompletedTrainings { get; set; }
        public DbSet<RecurringTraining> RecurringTrainings { get; set; }
        public DbSet<Reporting> Reportings { get; set; }

        // Configure relationships (if needed)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensuring foreign key relationships (if necessary)
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

            modelBuilder.Entity<Reporting>()
                .HasOne(r => r.Supervisor)
                .WithMany()
                .HasForeignKey(r => r.EmpNo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reporting>()
                .HasOne(r => r.Subordinate)
                .WithMany()
                .HasForeignKey(r => r.ReportingEmpNo)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
