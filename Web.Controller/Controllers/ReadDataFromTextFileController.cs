using System.Web.Mvc;
using Web.Model.ReadDataFromTextFile;

namespace Web.Controller.Controllers
{
    public class ReadDataFromTextFileController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var model = new DropDownListOfCountysDataModel();

            var displayModel = new DisplayDropDownMenuViewModel
            {
                County          = model.Counties,
                ListOfCountys   = model.ListOfCounty
            };

            return View(displayModel);
        }
    }
}