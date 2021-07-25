using System;
using System.Globalization;
using System.Web.Mvc;

namespace Web.Controller.Controllers
{
    public class MvcCachingController : System.Web.Mvc.Controller
    {
        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult MvcCachingExample(int id)
        {
            string returnDateTime           = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            TempData["DisplayDateTime"]     = returnDateTime;

            return View("~/Views/Caching/CachingView.cshtml");
        }
    }

    public class MvcCachingUsingMasterController : MasterController
    {
        [HttpGet]
        [OutputCache(CacheProfile = "CacheExample")]
        public ActionResult MvcCacheProfileExample()
        {
            string returnDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            TempData["DisplayDateTime"] = returnDateTime;
            return View("~/Views/Caching/CachingProfileView.cshtml");
        }

        [HttpGet]
        //[OutputCache(CacheProfile = "CacheExample")]
        public ActionResult MvcCacheProfileButNotForRenderActionPartialsViewsExample()
        {
            return View("~/Views/caching/CachingWithRenderActionPartial.cshtml");
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult MvcCacheProfileWillThrowErrorWithRenderAction()
        {
            string returnDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            TempData["DisplayDateTime"] = returnDateTime;
            return PartialView("~/Views/Partials/pvWillFailForCacheProfile.cshtml");
        }
    }
}
