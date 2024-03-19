using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{
    public class IntroduceController : BaseController<IntroduceController>
    {
        private readonly IS_News _s_News;
        private readonly IS_NewsCategory _s_NewsCategory;
        private readonly IOptions<Config_MetaSEO> _metaSEO;
        private readonly IS_OrganizationalComposition _s_OrganizationalComposition;
        private readonly IS_Person _s_Person;
        /*private readonly IS_PersonOrgComposition _s_PersonOrgComposition;*/
        private const int RECORDS_INTRODUCE = 6;
        private const int LATEST_RECORDS_INTRODUCE = 6;
        private const int RECORD_NUMBER_RELATE = 10;

        public IntroduceController(IS_News news, IS_NewsCategory newsCategory, IOptions<Config_MetaSEO> metaSEO, IS_OrganizationalComposition organizationalComposition, IS_Person person)
        {
            _s_News = news;
            _s_NewsCategory = newsCategory;
            _metaSEO = metaSEO;
            _s_OrganizationalComposition = organizationalComposition;
            _s_Person = person;
        }

        public async Task<IActionResult> Index(int? c, int page = 1)
        {
            ViewBag.CategoryId = c;
            ViewBag.Page = page;
            ViewBag.Record = RECORDS_INTRODUCE;

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

            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Introduce);
            var resLatestIntroduce = await _s_News.getListLatestNews((int)EN_NewsCategoryTypeId.Introduce, LATEST_RECORDS_INTRODUCE);
            return View(resLatestIntroduce.data ?? new List<M_News>());
        }

        public async Task<JsonResult> GetList(int? c, int page = 1)
        {
            var res = await _s_News.getListNewsTypeIdPagination((int)EN_NewsCategoryTypeId.Introduce, c, page, RECORDS_INTRODUCE);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }

        public async Task<IActionResult> Detail(int id)
        {
            string metaTitle = Empty;
            string metaDescription = Empty;
            string metaKeyword = Empty;
            string metaImage = Empty;

            var res = await _s_News.getNewsByIdWithListRelated(id, (int)EN_NewsCategoryTypeId.Introduce, RECORD_NUMBER_RELATE);
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
                    Title = IsNullOrEmpty(metaTitle) ? "Giới thiệu" : metaTitle,
                    Description = IsNullOrEmpty(metaDescription) ? "Giới thiệu" : metaDescription,
                    Keywords = IsNullOrEmpty(metaKeyword) ? "Giới thiệu" : metaKeyword,
                    Image = metaImage,
                });

                ViewBag.ListNewsRelated = res.data2nd?.newsObjsRef.ToObject<List<M_News>>();
                ViewBag.ListNewsRelated = res.data2nd?.newsObjsRef.ToObject<List<M_News>>();
                return View(res.data);
            }
            else
            {
                return Redirect("/no-data");
            }
        }

        public async Task<IActionResult> Organizational()
        {
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Organizational);
            /* var res = await _s_PersonOrgComposition.getListPersonOrgComposition();
             if (res.result == 1 && res.data != null)
             {
                 ViewBag.OrganizationalComposition = res.data.Select(x => x.organizationalCompositionObj?.name)?.Distinct().ToList();
                 return View(res.data);
             }*/
            var res = await _s_OrganizationalComposition.getListOrganizationalComposition();
            if (res.result == 1 && res.data != null)
            {
                ViewBag.OrganizationalComposition = res.data.Select(x => x.name)?.Distinct().ToList();
                return View(res.data);
            }
            else
            {
                return Redirect("/no-data");
            }
        }

        public async Task<JsonResult> DetailPerson(int id)
        {
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEODefaultAll(this, _metaSEO.Value.Member);
            var res = await _s_Person.getPersonById(id);
            return Json(new M_JResult(res, res.data, res.data2nd ?? 0));
        }
    }
}