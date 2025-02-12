using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOCL_Training_Module.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [StringLength(50)]
        public required string EmpNo { get; set; }  // Primary Key

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(100)]
        public string? FatherName { get; set; }  // Nullable, as it's not marked required

        [Required]
        [StringLength(100)]
        public required string EmpDepartment { get; set; }

        [Required]
        [StringLength(100)]
        public required string EmpDesignation { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        [RegularExpression("[MF]", ErrorMessage = "Sex must be either 'M' or 'F'")]
        public required string Sex { get; set; }  // Ensuring 'M' or 'F'

        [Required]
        public bool Reporting { get; set; }  // Boolean (true for "Yes", false for "No")

        [StringLength(50)]
        public string? ControllingEmp { get; set; }  // Nullable, since it can be null
    }
}
