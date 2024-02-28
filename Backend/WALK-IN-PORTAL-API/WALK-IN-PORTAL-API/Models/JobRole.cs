
namespace WALK_IN_PORTAL_API.Models
{
    public class JobRole
    {
        public int Id { get; set; }
        public string roleName { get; set; }
        public string roleDescription { get; set; }
        public string roleRequirement { get; set; }
        public float roleCompensation { get; set; }
        public int job_Id { get; set; }
    }
}
