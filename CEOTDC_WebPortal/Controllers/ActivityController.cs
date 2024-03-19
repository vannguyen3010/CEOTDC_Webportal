using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{

    public class ActivityController : BaseController<ActivityController>
    {
        private readonly IS_News _s_News;
        private readonly IOptions<Config_MetaSEO> _metaSEO;
        private const int RECORD_NUMBER = 10;

        public ActivityController(IS_News news, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_News = news;
            _metaSEO = metaSEO;
        }

        public async Task<IActionResult> Index(int? id)
        {
            string metaTitle = Empty;
            string metaDescription = Empty;
            string metaKeyword = Empty;
            string metaImage = Empty;

            var res = await _s_News.getActivityByIdIsHotWithListRelated(id, 1, RECORD_NUMBER);
            if (res.result == 1 && res.data != null)
            {
                if (res.result == 1 && res.data != null)
                {
                    ViewBag.categoryTitle = res.data.title;
                    metaTitle = res.data.metaTitle ?? res.data.title;
                    metaDescription = res.data.metaDescription ?? res.data.description;
                    metaKeyword = res.data.metaKeyword ?? res.data.title;
                    metaImage = res.data.imageObj?.mediumUrl;
                }

                ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEOCustom(this, new ViewModels.VM_ViewDataSEO
                {
                    Title = IsNullOrEmpty(metaTitle) ? "Hoạt động" : metaTitle,
                    Description = IsNullOrEmpty(metaDescription) ? "Hoạt động" : metaDescription,
                    Keywords = IsNullOrEmpty(metaKeyword) ? "Hoạt động" : metaKeyword,
                    Image = metaImage,
                });


                ViewBag.ListRelated = res.data2nd?.newsObjsRef.ToObject<List<M_News>>();
            }
            else
            {
                return Redirect("/no-data");
            }
            return View(res.data);
        }
    }
}
