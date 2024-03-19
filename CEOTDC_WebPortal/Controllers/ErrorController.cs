using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEOTDC_WebPortal.Controllers
{
	public class ErrorController : BaseController<ErrorController>
	{
        private static List<int> listStatusCode = new List<int> { 400, 404, 408, 500, 503 };
        public IActionResult Index(int statusCode)
        {
            if (listStatusCode.Contains(statusCode))
            {
                if (statusCode == 408)
                {
                    TempData["Timeout"] = "Phiên làm việc đã hết hạn, vui lòng đăng nhập lại.";
                    return Redirect("/account/signout");
                }
                return View($"~/Views/Error/{statusCode}.cshtml");
            }
            return View("~/Views/Error/404.cshtml");
        }
    }
}
