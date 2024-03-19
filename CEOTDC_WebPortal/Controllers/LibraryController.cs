using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Drawing.Imaging;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{
    public class LibraryController : BaseController<LibraryController>
    {
        private const int RECORDS_LIBRARY = 9;
        private readonly IS_Document _s_Document;
        private readonly IS_DocumentFile _s_DocumentFile;
        private readonly IS_DocumentSubject _s_DocumentSubject;
        private readonly IOptions<Config_MetaSEO> _metaSEO;

        public LibraryController(IS_Document document, IS_DocumentFile documentFile, IS_DocumentSubject documentSubject, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_Document = document;
            _s_DocumentFile = documentFile;
            _s_DocumentSubject = documentSubject;
            _metaSEO = metaSEO;
        }

        public async Task<IActionResult> Index(int? c, int page = 1)
        {
            ViewBag.CategoryId = c;
            ViewBag.Page = page;
            ViewBag.Record = RECORDS_LIBRARY;

            string metaTitle = Empty;
            string metaDescription = Empty;
            string metaKeyword = Empty;
            string metaImage = Empty;

            if (c != null)
            {
                var res = await _s_DocumentSubject.getDocumentSubjectById(c.Value);
                if (res.result == 1 && res.data != null)
                {
                    ViewBag.CategoryName = res.data.name;
                    metaTitle = res.data.name;
                    metaDescription = res.data.name;
                    metaKeyword = res.data.name;
                    metaImage = res.data.imageObj?.mediumUrl;
                }
            }

            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Library);
            return View();
        }

        public async Task<JsonResult> GetList(int? c, int page = 1)
        {
            var res = await _s_Document.getListDocumentByDocumentSubjectId(c, RECORDS_LIBRARY, page);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }

        public async Task<IActionResult> Detail(int id)
        {
            string metaTitle = Empty;
            string metaDescription = Empty;
            string metaKeyword = Empty;
            string metaImage = Empty;

            var res = await _s_Document.getDocumentById<M_Document>(id);
            if (res.result == 1 && res.data != null)
            {
                if (res.result == 1 && res.data != null)
                {
                    ViewBag.categoryTitle = res.data.name;
                    metaTitle = res.data.metaTitle ?? res.data.name;
                    metaDescription = res.data.metaDescription ?? res.data.summaryDetail;
                    metaKeyword = res.data.metaKeyword ?? res.data.name;
                    metaImage = res.data.imageObj?.mediumUrl;
                }

                ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEOCustom(this, new ViewModels.VM_ViewDataSEO
                {
                    Title = IsNullOrEmpty(metaTitle) ? "Thư viện" : metaTitle,
                    Description = IsNullOrEmpty(metaDescription) ? "Thư viện" : metaDescription,
                    Keywords = IsNullOrEmpty(metaKeyword) ? "Thư viện" : metaKeyword,
                    Image = metaImage,
                });

                ViewBag.ListLibraryRelated = res.data2nd?.documentRelatedObjs.ToObject<List<M_Document>>();
              return View(res.data);
            }
            else
            {
                return Redirect("/no-data");
            }
        }
    }
}
