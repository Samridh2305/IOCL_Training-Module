using IOCL_Training_Module.Models;

public class RecurringTask
{
    public int SrNo { get; set; }
    public string EmpNo { get; set; }
    public string TrainingID { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public Employee Employee { get; set; }
    public Training Training { get; set; }
}
