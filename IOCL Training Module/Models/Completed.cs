namespace IOCL_Training_Module.Models
{
    public class Completed
    {
        public int SrNo { get; set; } 
        public string EmpNo { get; set; }  
        public string CompletedTraining { get; set; }  
        public DateTime FromDate { get; set; } 
        public DateTime ToDate { get; set; }  
        public Employee Employee { get; set; }
        public Training Training { get; set; }
    }

}
