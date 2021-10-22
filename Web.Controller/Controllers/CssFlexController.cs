using System.Web.Mvc;

namespace Web.Controller.Controllers
{
    public class CssFlexController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
    }
}
