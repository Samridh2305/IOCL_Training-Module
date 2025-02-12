using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOCL_Training_Module.Models
{
    public class Completed
    {
        [Key]
        public int SrNo { get; set; }  // Primary Key (Auto-Increment)

        [Required]
        [ForeignKey("Employee")]
        public required string EmpNo { get; set; }  // Foreign Key - Employee

        public Employee? Employee { get; set; }  // Navigation Property

        [Required]
        [ForeignKey("Training")]
        public required string CompletedTraining { get; set; }  // Foreign Key - Training

        public Training? Training { get; set; }  // Navigation Property

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }  // Training Start Date

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }  // Training End Date
    }
}
