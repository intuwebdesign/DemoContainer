using System.Web.Mvc;
using Web.Model.FormValidationModel;

namespace Web.Controller.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormValidationViewModel model)
        {
            //You would do server validation here
            if (Request.IsAjaxRequest())
            {
                return Json(new { Confirm = true, isJsonErrorList = false, message = $"{model.FirstName} Form Posted" }, JsonRequestBehavior.AllowGet);
            }

            return View();
        }
    }
}