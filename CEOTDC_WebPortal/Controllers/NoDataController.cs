using Microsoft.AspNetCore.Mvc;

namespace CEOTDC_WebPortal.Controllers
{
    public class NoDataController : BaseController<NoDataController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
