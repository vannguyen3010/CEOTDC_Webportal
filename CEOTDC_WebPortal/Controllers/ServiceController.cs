using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{
    public class ServiceController : BaseController<ServiceController>
    {
        private readonly IS_News _s_News;
        private readonly IS_NewsCategory _s_NewsCategory;
        private readonly IOptions<Config_MetaSEO> _metaSEO;
        private const int RECORDS_SERVICE = 6;
        private const int LATEST_RECORDS_SERVICE = 6;
        private const int RECORD_SERVICE_RELATED = 10;

        public ServiceController(IS_News news, IS_NewsCategory newsCategory, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_News = news;
            _s_NewsCategory = newsCategory;
            _metaSEO = metaSEO;
        }

        public async Task<IActionResult> Index(int? c, int page = 1)
        {
            ViewBag.CategoryId = c;
            ViewBag.Page = page;
            ViewBag.Record = RECORDS_SERVICE;

            string metaTitle = Empty;
            string metaDescription = Empty;
            string metaKeyword = Empty;
            string metaImage = Empty;

            if (c.HasValue)
            {
                var res = await _s_NewsCategory.getNewsCategoryById(c.Value);
                if (res.result == 1 && res.data != null)
                {
                    ViewBag.CategoryName = res.data.name;
                    metaTitle = res.data.name;
                    metaDescription = res.data.name;
                    metaKeyword = res.data.name;
                    metaImage = res.data.imageObj?.mediumUrl;
                }
            }

            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Service);
            var resLatestNews = await _s_News.getListLatestNews((int)EN_NewsCategoryTypeId.Service, LATEST_RECORDS_SERVICE);
            return View(resLatestNews.data ?? new List<M_News>());
        }

        public async Task<JsonResult> GetList(int? c, int page = 1)
        {
            var res = await _s_News.getListNewsTypeIdPagination((int)EN_NewsCategoryTypeId.Service, c, page, RECORDS_SERVICE);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }

        public async Task<IActionResult> Detail(int id)
        {
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Service);
            var res = await _s_News.getNewsByIdWithListRelated(id, (int)EN_NewsCategoryTypeId.Service, RECORD_SERVICE_RELATED);
            if (res.result == 1 && res.data != null)
                ViewBag.ListServiceRelated = res.data2nd?.newsObjsRef.ToObject<List<M_News>>();
            return View(res.data);
        }
    }
}