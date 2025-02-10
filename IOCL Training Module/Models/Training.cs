namespace IOCL_Training_Module.Models
{
    public class Training
    {
        public string TrainingID { get; set; }
        public string TrainingName { get; set; }
        public int Duration { get; set; }
        public string Recurring { get; set; }
        public string Venue { get; set; }
        public string Department { get; set; }
        public int? Validity { get; set; }  // Nullable
    }

}
