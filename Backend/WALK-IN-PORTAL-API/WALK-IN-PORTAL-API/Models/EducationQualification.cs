namespace WALK_IN_PORTAL_API.Models
{
    public class EducationQualification
    {
        public int Id { get; set; }
        public float percentage { get; set; } = 0.0f;
        public int passingYear { get; set; } = 0;
        public string qualification { get; set; } = string.Empty;
        public string stream { get; set; } = string.Empty;
        public string college { get; set; } = string.Empty;
        public string? otherCollege { get; set; } = string.Empty;
        public string collegeLocation { get; set; } = string.Empty;
        public int userId { get; set; } = 0;
        public DateTime dateCreated { get; set; } = DateTime.Now;
        public DateTime dateUpdated { get; set; }
    }
}
