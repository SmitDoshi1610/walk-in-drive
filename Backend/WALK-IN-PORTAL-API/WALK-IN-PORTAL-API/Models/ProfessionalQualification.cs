namespace WALK_IN_PORTAL_API.Models
{
    public class ProfessionalQualification
    {
        public int Id { get; set; }
        public string applicationType { get; set; } = "";
        public List<string>? fresherTechnologiesFamiliar { get; set; } = new List<string>();
        public string? fresherOtherFamiliar { get; set; } = "";
        public string? othersExpertise {  get; set; } = string.Empty;
        public List<string>? experienceTechnologiesFamiliar { get; set; } = new List<string>();
        public string? experienceOtherFamiliar { get; set; } = string.Empty;
        public List<string>? technologiesExpert { get; set; } = new List<string>();
        public int yearsOfExperience { get; set; } = 0;
        public string? currentCTC { get; set; } = string.Empty;
        public string? expectedCTC { get; set; } = string.Empty;
        public string isInNoticePeriod { get; set; } = string.Empty;
        public DateTime? noticePeriodEnd { get; set; } = DateTime.Now;
        public int noticePeriodDuration { get; set; } = 0;
        public string haveAppliedTestBefore { get; set; } = string.Empty;
        public string fresherRoleAppliedFor { get; set; } = string.Empty; 
        public string experienceRoleAppliedFor { get; set; } = string.Empty;
        public DateTime? dateCreated { get; set; } = DateTime.Now;
        public DateTime? dateUpdated { get; set; } = DateTime.Now;
    }
}
