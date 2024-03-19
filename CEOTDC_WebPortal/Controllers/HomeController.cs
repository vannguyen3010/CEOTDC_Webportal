using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CEOTDC_WebPortal.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IS_News _s_News;
        private readonly IS_PartnerList _s_PartnerList;
        private readonly IOptions<Config_MetaSEO> _metaSEO;

        public HomeController(IS_News news, IS_PartnerList partnerList, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_News = news;
            _s_PartnerList = partnerList;
            _metaSEO = metaSEO;
        }

        public async Task<IActionResult> Index()
        {
            var resNews = await _s_News.getListNewsBySupplierId(CommonConstants.OWNER_SUPPLIER_ID, 5, 5, 10);
            var resPartnerList = await _s_PartnerList.getListPartnerList();
            ViewBag.PartnerList = resPartnerList.data ?? new List<M_PartnerList>();
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Home);
            return View(resNews.data ?? new M_NewsHome());
        }
    }
}