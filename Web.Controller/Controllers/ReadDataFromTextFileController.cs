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


        [HttpPost, ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult InsertText(DisplayDropDownMenuViewModel model)
        {
            bool addLineToFile = WriteToTextFile.WriteToFile(model);
            if (Request.UrlReferrer == null) return null;

            string redirectBackToPage = Request.UrlReferrer.PathAndQuery;

            if (addLineToFile)
            {
                TempData["Status"] = $"Added {model.InputText} to text file";
                return Redirect(redirectBackToPage);
            }
            TempData["Status"] = $"Failed to add {model.InputText} to text file";
            return Redirect(redirectBackToPage);
        }
    }
}