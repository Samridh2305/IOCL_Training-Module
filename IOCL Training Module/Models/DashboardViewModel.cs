using System.Collections.Generic;

namespace IOCL_Training_Module.Models
{
    public class DashboardViewModel
    {
        public Employee? Employee { get; set; }
        public List<Completed> CompletedTrainings { get; set; } = new List<Completed>();
        public List<NotCompleted> NotCompletedTrainings { get; set; } = new List<NotCompleted>();
        public List<RecurringTraining> RecurringTrainings { get; set; } = new List<RecurringTraining>(); 
    }
}
