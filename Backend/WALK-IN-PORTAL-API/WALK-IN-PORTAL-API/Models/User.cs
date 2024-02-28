namespace WALK_IN_PORTAL_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string firstName { get; set; } = string.Empty;

        public string lastName { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string phone { get; set; } = string.Empty;

        public string profilePicture { get; set; } = string.Empty;

        public string resume { get; set; } = string.Empty;

        public string portfolioUrl { get; set; } = string.Empty;

        public string referredBy { get; set; } = string.Empty;

        public bool emailNotification { get; set; } = false;

        public List<string> preferredJobRoles { get; set; } = new List<string>();

        public DateTime? dateCreated { get; set; } = DateTime.Now;

        public DateTime? dateUpdated { get; set; }
    }
}
