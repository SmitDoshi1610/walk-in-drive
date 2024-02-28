namespace WALK_IN_PORTAL_API.Models
{
    public class WalkInDrive
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string location { get; set; }
        public string timePreference { get; set; }
        public List<string> jobPreference { get; set; } = new List<string>();
        public int user_Id { get; set; }
        public List<JobRole> walkInJob { get; set; } = new List<JobRole>();
     
    }
}
