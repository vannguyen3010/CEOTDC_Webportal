namespace CEOTDC_WebPortal.Models
{
    public class M_OrganizationalComposition
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public string remark { get; set; }
        public DateTime updatedAt { get; set; }
        public List<M_PersonOrgComposition> personOrgCompositionObjs { get; set; }
    }
}