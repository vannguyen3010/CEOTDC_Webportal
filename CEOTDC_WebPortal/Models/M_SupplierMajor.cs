namespace CEOTDC_WebPortal.Models
{
    public class M_SupplierMajor
    {
        public int? id { get; set; }
        public int supplierId { get; set; }
        public int majorId { get; set; }
        public M_Major majorObj { get; set; }
        public M_Supplier supplierObj { get; set; }
    }
    public class M_SupplierMajorDetail : M_SupplierMajor
    {
        public List<M_Person> personObjs { get; set; }
    }
}
