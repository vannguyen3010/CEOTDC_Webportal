namespace CEOTDC_WebPortal.Models
{
    public class M_Document
    {
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string summaryDetail { get; set; }
        public string detail { get; set; }
        public string nameSlug { get; set; }
        public string remark { get; set; }
        public int? status { get; set; }
        public int? createdBy { get; set; }
        public int? updatedBy { get; set; }
        public string metaTitle { get; set; }
        public string metaKeyword { get; set; }
        public string metaDescription { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime timer { get; set; }
        public M_Document documentSubjectObj { get; set; }
        public M_Person departmentObj { get; set; }
        public M_Image imageObj { get; set; }
        public M_Person memberObj { get; set; }
        public List<M_Document> documentRelatedObjs { get; set; }
        public List<M_DocumentFile> documentFileObjs { get; set; }
    }
}
