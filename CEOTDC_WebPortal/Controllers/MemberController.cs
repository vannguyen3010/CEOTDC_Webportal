using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{
    public class MemberController : BaseController<MemberController>
    {
        private readonly IS_SupplierMajor _s_SupplierMajor;
        private readonly IOptions<Config_MetaSEO> _metaSEO;
        private const int RECORDS_NUMBER = 20;

        public MemberController(IS_SupplierMajor supplierMajor, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_SupplierMajor = supplierMajor;
            _metaSEO = metaSEO;
        }

        public ActionResult Index(int? c, int page = 1)
        {
            c = c == null ? -2 : c;
            ViewBag.record = RECORDS_NUMBER;
            ViewBag.page = page;
            ViewBag.CategoryId = c;
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Member);
            return View();
        }

        public async Task<JsonResult> GetList(int? majorId, int page = 1)
        {
            var res = await _s_SupplierMajor.getListSupplierMajorBySequenceStatus(majorId, CommonConstants.SEQUENCESTATUS, page, RECORDS_NUMBER);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }

        public async Task<JsonResult> Detail(int id)
        {
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Member);
            var res = await _s_SupplierMajor.getSupplierMajorById(id);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }
    }
}
