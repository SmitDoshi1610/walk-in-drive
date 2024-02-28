namespace WALK_IN_PORTAL_API.Models
{
    public class AppliedJobRole
    {
        public int userId {  get; set; }

        public int driveId { get; set; }

        public string timeSlot { get; set; }
        public string resume {  get; set; }
        public List<string> jobPreference { get; set; } = new List<string>();
    }
}
