using AutoMapper;
using CEOTDC_WebPortal.Lib;
using CEOTDC_WebPortal.Models;
using CEOTDC_WebPortal.Services;
using CEOTDC_WebPortal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.String;

namespace CEOTDC_WebPortal.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private IMemoryCache memoryCache;
        private IS_Supplier s_Supplier;

        protected IMemoryCache _memoryCache => memoryCache ?? (memoryCache = HttpContext?.RequestServices.GetService<IMemoryCache>());

        protected IS_Supplier _s_Supplier => s_Supplier ?? (s_Supplier = HttpContext?.RequestServices.GetService<IS_Supplier>());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_SUPPLIER, out ResponseData<M_Supplier> supplier))
            {
                supplier = _s_Supplier.getSupplierById<M_Supplier>(CommonConstants.OWNER_SUPPLIER_ID).Result;
                if (supplier.result == 1 && supplier.data != null)
                {
                    MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                        Priority = CacheItemPriority.Normal,
                        //SlidingExpiration = TimeSpan.FromMinutes(5),
                        Size = 1024
                    };
                    _memoryCache.Set(CommonConstants.CACHE_KEY_SUPPLIER, supplier, cacheExpiryOptions);
                }
            }

            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_NEWS_CATEGORY, out ResponseData<List<M_NewsCategory>> newsCategory))
            {
                newsCategory = HttpContext?.RequestServices.GetService<IS_NewsCategory>().getListNewsCategory<List<M_NewsCategory>>().Result;
                if (newsCategory.result == 1 && newsCategory.data != null)
                {
                    MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                        Priority = CacheItemPriority.Normal,
                        //SlidingExpiration = TimeSpan.FromMinutes(5),
                        Size = 1024
                    };
                    _memoryCache.Set(CommonConstants.CACHE_KEY_NEWS_CATEGORY, newsCategory, cacheExpiryOptions);
                }
            }

            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_LIBRARY_CATEGORY, out ResponseData<List<M_DocumentSubject>> libraryCategory))
            {
                libraryCategory = HttpContext?.RequestServices.GetService<IS_DocumentSubject>().getListDocumentSubject<List<M_DocumentSubject>>().Result;
                if (newsCategory.result == 1 && newsCategory.data != null)
                {
                    MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                        Priority = CacheItemPriority.Normal,
                        //SlidingExpiration = TimeSpan.FromMinutes(5),
                        Size = 1024
                    };
                    _memoryCache.Set(CommonConstants.CACHE_KEY_LIBRARY_CATEGORY, newsCategory, cacheExpiryOptions);
                }
            }

            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_MEMBER_CATERGORY, out ResponseData<List<M_Major>> memberCategory))
            {
                memberCategory = HttpContext?.RequestServices.GetService<IS_Major>().getListMajorDropDown(CommonConstants.SEQUENCESTATUS).Result;
                if (memberCategory.result == 1 && memberCategory.data != null)
                {
                    MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                        Priority = CacheItemPriority.Normal,
                        //SlidingExpiration = TimeSpan.FromMinutes(5),
                        Size = 1024
                    };
                    _memoryCache.Set(CommonConstants.CACHE_KEY_MEMBER_CATERGORY, memberCategory, cacheExpiryOptions);
                }
            }

            ViewBag.MemberCategory = memberCategory.data ?? new List<M_Major>();
            ViewBag.SupplierInfo = supplier.data ?? new M_Supplier();
            ViewBag.NewsCategory = newsCategory.data ?? new List<M_NewsCategory>();
            ViewBag.LibraryCategory = libraryCategory.data ?? new List<M_DocumentSubject>();
            base.OnActionExecuting(context);
        }
    }
}
