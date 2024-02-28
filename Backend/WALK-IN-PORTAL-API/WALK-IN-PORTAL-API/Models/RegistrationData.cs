namespace WALK_IN_PORTAL_API.Models
{
    public class RegistrationData
    {
        public List<string> preferedJobRoles { get; set; } = new List<string>();
        public List<int> yearOfPassing { get; set; } = new List<int>();
        public List<string> qualification { get; set; } = new List<string>();
        public List<string> stream { get; set; } = new List<string>();

        public List<string> college { get; set; } = new List<string>();

        public List<string> tecnologiesExpert { get; set; } = new List<string>();

        public List<string> tecnologiesFamiliar { get; set; } = new List<string>();

        public List<int> noticePeriod { get; set; } = new List<int>();
    }
}
