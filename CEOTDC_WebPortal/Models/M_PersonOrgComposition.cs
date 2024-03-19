namespace CEOTDC_WebPortal.Models
{
    public class M_PersonOrgComposition
    {
        public int? id { get; set; }
        public int personId { get; set; }
        public int organizationalCompositionId { get; set; }
        public string summaryAchievement { get; set; }
        public string profileUrl { get; set; }
        public string metaTitle { get; set; }
        public string metaKeyword { get; set; }
        public string metaDescription { get; set; }
        public M_Person personObj { get; set; }
        public M_OrganizationalComposition organizationalCompositionObj { get; set; }
    }
}
