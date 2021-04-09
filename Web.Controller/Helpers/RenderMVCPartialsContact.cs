using System.IO;
using System.Web.Mvc;
using Web.Controller.Controllers;

namespace Web.Controller.Helpers
{
    public class RenderMVCPartialsContact
    {
        private readonly SendEmailController _sendEmailController;

        public RenderMVCPartialsContact(SendEmailController sendEmailController)
        {
            _sendEmailController = sendEmailController;
        }

        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = _sendEmailController.ControllerContext.RouteData.GetRequiredString("action");

            _sendEmailController.ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult     = ViewEngines.Engines.FindPartialView(_sendEmailController.ControllerContext, viewName);
                ViewContext viewContext         = new ViewContext(_sendEmailController.ControllerContext, viewResult.View, _sendEmailController.ViewData, _sendEmailController.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
