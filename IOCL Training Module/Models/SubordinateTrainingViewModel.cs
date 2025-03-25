using System.Collections.Generic;

namespace IOCL_Training_Module.Models
{
    public class SubordinateTrainingViewModel
    {
        public List<Completed> CompletedTrainings { get; set; } = new List<Completed>();

        // List of subordinates for dropdown
        public List<Employee> Subordinates { get; set; } = new List<Employee>();

        // Stores the currently selected employee number
        public string? SelectedEmpNo { get; set; }
    }
}
