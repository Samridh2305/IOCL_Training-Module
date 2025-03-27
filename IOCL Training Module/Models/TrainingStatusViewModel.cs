namespace IOCL_Training_Module.Models
{
    public class TrainingStatusViewModel
    {
        public required List<Completed> CompletedTrainings { get; set; }
        public required List<NotCompleted> NotCompletedTrainings { get; set; }
        public required List<RecurringTraining> UpcomingTrainings { get; set; }
        public required List<Employee> Subordinates { get; set; } // List of subordinates
        public string? SelectedEmpNo { get; set; } // Selected employee number

        // 🔹 Filters for Completed Trainings
        public string? TrainingName { get; set; }
        public string? Venue { get; set; }
        public string? Type { get; set; } // Selected Training Type
        public string? Department { get; set; }
        public string? Designation { get; set; } // 🔹 New filter for Employee Designation
        public string? Sex { get; set; } // 🔹 New filter for Employee Gender
        public string? Status { get; set; }
        public string? SafetyTraining { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        // 🔹 Training Type Dropdown (Predefined List)
        public List<string> TrainingTypes { get; } = new()
        {
            "General Awareness",
            "Functional",
            "Developmental Training"
        };

        // 🔹 Dropdown Options for Filtering
        public List<string>? Departments { get; set; } // Populated from DB
        public List<string>? Designations { get; set; } // Populated from DB
    }
}
