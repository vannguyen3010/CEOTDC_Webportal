using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Drawing.Imaging;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{
    public class NewsController : BaseController<NewsController>
    {
        private readonly IS_News _s_News;
        private readonly IS_NewsCategory _s_NewsCategory;
        private readonly IOptions<Config_MetaSEO> _metaSEO;
        private const int RECORDS_NEWS = 6;
        private const int LATEST_RECORDS_NEWS = 6;
        private const int RECORD_NUMBER_RELATE = 10;

        public NewsController(IS_News news, IS_NewsCategory newsCategory, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_News = news;
            _s_NewsCategory = newsCategory;
            _metaSEO = metaSEO;
        }

        public async Task<IActionResult> Index(int? c, int page = 1)
        {
            ViewBag.CategoryId = c;
            ViewBag.Page = page;
            ViewBag.Record = RECORDS_NEWS;

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

            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.News);
            var resLatestNews = await _s_News.getListLatestNews((int)EN_NewsCategoryTypeId.News, LATEST_RECORDS_NEWS);
            return View(resLatestNews.data ?? new List<M_News>());
        }

        public async Task<JsonResult> GetList(int? c, int page = 1)
        {
            var res = await _s_News.getListNewsTypeIdPagination((int)EN_NewsCategoryTypeId.News, c, page, RECORDS_NEWS);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }

        public async Task<IActionResult> Detail(int id)
        {
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.News);
            var res = await _s_News.getNewsByIdWithListRelated(id, (int)EN_NewsCategoryTypeId.News, RECORD_NUMBER_RELATE);
            if (res.result == 1 && res.data != null)
            ViewBag.ListNewsRelated = res.data2nd?.newsObjsRef.ToObject<List<M_News>>();
            return View(res.data);
        }
    }
}
