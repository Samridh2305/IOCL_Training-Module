using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOCL_Training_Module.Models
{
    [Table("Reporting")] // Explicitly mapping to SQL Server table
    public class EmployeeReporting  // Changed class name from Reporting to EmployeeReporting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SrNo { get; set; }  // Primary Key (Auto-Increment)

        [Required]
        public required string EmpNo { get; set; }  // Foreign Key - Employee (NOT NULL)

        [Required]
        public required string Reporting { get; set; }  // Foreign Key - Reporting Employee (NOT NULL)

        [Required]
        public required DateTime FromDate { get; set; }  // Reporting Start Date (NOT NULL)

        [Required]
        public required DateTime ToDate { get; set; }  // Reporting End Date (NOT NULL)

        // Navigation Properties (Nullable to avoid runtime errors)
        [ForeignKey("EmpNo")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("Reporting")]
        public virtual Employee? Manager { get; set; }  // Self-referencing Employee
    }
}
