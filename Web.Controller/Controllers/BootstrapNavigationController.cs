using System.Linq;
using System.Web.Mvc;
using Web.Model.DynamicNavigationModel;

namespace Web.Controller.Controllers
{
    public class BootstrapNavigationController : System.Web.Mvc.Controller
    {
        public ActionResult BootstrapNav()
        {
            return View("~/Views/DemoNav/BootstrapNav.cshtml");
        }

        public ActionResult DisplayNavigation()
        {
            var dataModel = new BootstrapNavigtaionDataModel();

            var displayData = dataModel.RetrieveMenuData().ToList();

            var model = new DisplayBootstrapNavigtaionViewModel
            {
                DisplayNavigation = displayData

            };

            return PartialView("~/Views/Partials/pvDynamicMenu.cshtml",model);
        }
    }
}
