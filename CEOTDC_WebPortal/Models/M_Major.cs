namespace CEOTDC_WebPortal.Models
{
    public class M_Major
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public int? reOrder { get; set; }
        public M_Supplier supplierObj { get; set; }
       /* public M_Major majorObj { get; set; }*/
    }
}