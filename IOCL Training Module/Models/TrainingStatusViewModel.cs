namespace IOCL_Training_Module.Models
{
    public class TrainingStatusViewModel
    {
        public required List<Completed> CompletedTrainings { get; set; }
        public required List<NotCompleted> NotCompletedTrainings { get; set; }
        public required List<Employee> Subordinates { get; set; } // List of subordinates
        public string? SelectedEmpNo { get; set; } // Selected employee number
        public required List<RecurringTraining> UpcomingTrainings { get; set; }
    }

