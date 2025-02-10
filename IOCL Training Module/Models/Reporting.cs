namespace IOCL_Training_Module.Models
{
    public class Reporting
    {
        public int SrNo { get; set; }  
        public string EmpNo { get; set; }  
        public string ReportingEmpNo { get; set; }  
        public Employee Supervisor { get; set; }
        public Employee Subordinate { get; set; }
    }

}
