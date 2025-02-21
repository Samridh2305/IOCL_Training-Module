using System;
using System.ComponentModel.DataAnnotations;

namespace IOCL_Training_Module.Models
{
    public class Training
    {
        [Key]
        [Required]
        [StringLength(50)]
        public required string TrainingID { get; set; }

        [Required]
        [StringLength(200)]
        public required string TrainingName { get; set; }

        [Required] 
        [Range(1, 365)]
        public int Duration { get; set; }

        [Required]
        [StringLength(100)]
        public required string Venue { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [Range(1, 120)]
        public int? Validity { get; set; }

        // 🔹 Add These Properties
        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
    }
}
