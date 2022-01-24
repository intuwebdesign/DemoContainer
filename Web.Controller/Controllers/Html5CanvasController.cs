using System;
using System.IO;
using System.Web.Mvc;
using Web.Model.Html5CanvasModel;

namespace Web.Controller.Controllers
{
    public class Html5CanvasController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Base64Form(Html5CanvasViewModel model)
        {
            var base64Image = model.Base64Image;

            Guid fileName = Guid.NewGuid();

            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), $"{fileName}.png");

            CreateImageFromBase64.CreateImage(path,base64Image);

            TempData["Image"] = $"/UploadedFiles/{fileName}.png";

            string redirectBackToPage = Request.UrlReferrer.PathAndQuery;
            return Redirect(redirectBackToPage);
        }
    }
}
