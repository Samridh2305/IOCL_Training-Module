namespace IOCL_Training_Module.Models
{
    public class NotCompleted
    {
        public int SNo { get; set; }  
        public string EmpNo { get; set; }  
        public string Code { get; set; }  
        public Employee Employee { get; set; }
        public Training Training { get; set; }
    }

}
