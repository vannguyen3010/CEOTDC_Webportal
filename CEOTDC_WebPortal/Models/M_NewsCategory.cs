namespace CEOTDC_WebPortal.Models
{
    public class M_NewsCategory
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? typeId { get; set; } //0:News | 1:Introduce | 2: Service | 4: Activity
        public string name { get; set; }
        public string nameSlug { get; set; }
        public int? reOrder { get; set; }
        public M_Image imageObj { get; set; }
    }
    public class M_NewsCategoryMenu
    {
        public List<M_NewsCategory> newsCategoryServiceObjs { get; set; }
        public List<M_NewsCategory> newsCategoryIntroduceObjs { get; set; }
        public List<M_NewsCategory> newsCategoryObjs { get; set; }

    }
    public enum EN_NewsCategoryTypeId
    {
        News,
        Introduce,
        Service,
        Activies,
    }
}
