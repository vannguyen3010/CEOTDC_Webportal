namespace CEOTDC_WebPortal.Models
{
    public class M_NewsHome
    {
        public List<M_News> newsIntroduceObjs { get; set; }
        public List<M_News> newsObjs { get; set; }
        public List<M_News> newsActivityObjs { get; set; }
    }
    public class M_News
    {
        public int? id { get; set; }
        public string title { get; set; }
        public int? categoryId { get; set; }
        public int? supplierId { get; set; }
        public string titleSlug { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public int? reOrder { get; set; }
        public DateTime? publishedAt { get; set; }
        public M_NewsCategory categoryObj { get; set; }
        public M_Image imageObj { get; set; }
        public List<M_Image> imageListObj { get; set; }
        public string metaTitle { get; set; }
        public string metaKeyword { get; set; }
        public string metaDescription { get; set; }
    }
}
