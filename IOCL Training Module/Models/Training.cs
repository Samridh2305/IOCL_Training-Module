using System;
using System.ComponentModel.DataAnnotations;

namespace IOCL_Training_Module.Models
{
    public class Training
    {
        [Key]
        [Required]
        [StringLength(50)] // Adjust length based on database constraints
        public required string TrainingID { get; set; }  // Primary Key (varchar)

        [Required]
        [StringLength(200)]
        public required string TrainingName { get; set; }  // Training Name (nvarchar)

        [Required]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days.")]
        public int Duration { get; set; }  // Duration (int)

        [Required]
        [StringLength(100)]
        public required string Venue { get; set; }  // Venue (nvarchar)

        [StringLength(100)]
        public string? Department { get; set; }  // Nullable (nvarchar)

        [Range(1, 120, ErrorMessage = "Validity must be between 1 and 120 months.")]
        public int? Validity { get; set; }  // Nullable (Months)
    }
}
