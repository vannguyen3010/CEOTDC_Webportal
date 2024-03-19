namespace CEOTDC_WebPortal.Models
{
    public class M_DocumentSubject
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? typeId { get; set; } //0:News | 1:Introduce | 2: Service | 3: Activity 
        public string name { get; set; }
        public string nameSlug { get; set; }
        public int? reOrder { get; set; }
        public M_Image imageObj { get; set; }
    }
}
