using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOCL_Training_Module.Models
{
    [Table("RecurringTraining")] 
    public class RecurringTraining
    {
        [Key]
        public int SrNo { get; set; }  // Primary Key (Auto-Increment)

        [Required]
        public required string EmpNo { get; set; }  // Foreign Key - Employee (NOT NULL)

        [Required]
        public required string TrainingID { get; set; }  // Foreign Key - Training (NOT NULL)

        [Required]
        public required DateTime FromDate { get; set; }  // Training Start Date (NOT NULL)

        [Required]
        public required DateTime ToDate { get; set; }  // Training End Date (NOT NULL)

        public DateTime? NextTrainingDate { get; set; }  // Calculated Next Training Date

        // Navigation Properties (Nullable to avoid runtime errors)
        [ForeignKey("EmpNo")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("TrainingID")]
        public virtual Training? Training { get; set; }
    }
}
