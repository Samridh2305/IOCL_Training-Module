using System.Collections.Generic;

namespace IOCL_Training_Module.Models
{
    public class DashboardViewModel
    {
        public Employee? Employee { get; set; } // Selected employee
        public Employee? LoggedInEmployee { get; set; } // Logged-in user
        public List<Employee?> ReportingEmployees { get; set; } = new List<Employee?>();
        public Employee? ReportingManager { get; set; } // Reporting manager of selected employee
        public List<Completed> CompletedTrainings { get; set; } = new List<Completed>();
        public List<NotCompleted> NotCompletedTrainings { get; set; } = new List<NotCompleted>();
        public List<RecurringTraining> RecurringTrainings { get; set; } = new List<RecurringTraining>();
    }
}
