using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOCL_Training_Module.Models
{
    [Table("RecurringTraining")] // Explicitly map to existing table
    public class RecurringTraining
    {
        [Key]
        public int SrNo { get; set; }  // Primary Key (Assumed Auto-Increment in SQL Server)

        [Required]
        public required string EmpNo { get; set; }  // Foreign Key - Employee (NOT NULL)

        [Required]
        public required string TrainingID { get; set; }  // Foreign Key - Training (NOT NULL)

        [Required]
        public required DateTime FromDate { get; set; }  // Training Start Date (NOT NULL)

        [Required]
        public required DateTime ToDate { get; set; }  // Training End Date (NOT NULL)

        // Navigation Properties (Nullable to avoid runtime errors)
        [ForeignKey("EmpNo")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("TrainingID")]
        public virtual Training? Training { get; set; }
    }
}
