using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOCL_Training_Module.Models
{
    public class NotCompleted
    {
        [Key]
        public int SNo { get; set; }  // Primary Key (Auto-Increment)

        [Required]
        [ForeignKey("Employee")]
        public required string EmpNo { get; set; }  // Foreign Key - Employee

        public Employee? Employee { get; set; }  // Navigation Property

        [Required]
        [ForeignKey("Training")]
        public required string Code { get; set; }  // Foreign Key - TrainingID in Training Table

        public Training? Training { get; set; }  // Navigation Property
    }
}
