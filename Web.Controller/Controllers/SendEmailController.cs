using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Controller.Helpers;
using Web.Model.EmailAttachmentModel;

namespace Web.Controller.Controllers
{
    public class SendEmailController : System.Web.Mvc.Controller
    {
        private readonly RenderMVCPartialsContact _renderMvcPartialsContact;

        public SendEmailController()
        {
            _renderMvcPartialsContact = new RenderMVCPartialsContact(this);
        }


        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost, ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(EmailBodyViewModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                //Not necessary, but another way to get hold of validation errors
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                List<string> errorList = new List<string>();

                var modelErrors = allErrors as ModelError[] ?? allErrors.ToArray();
                errorList.Add("<ul class=\"list-unstyled\">");
                foreach (var error in modelErrors)
                {
                    errorList.Add($"<li class=\"text-danger\">{error.ErrorMessage}</li>");
                }
                errorList.Add("</ul>");

                string displayErrors = string.Join(" ", errorList.ToArray());
                TempData["ErrorList"] = displayErrors;

                return View("Home");
            }

            EmailBodyViewModel emailBodyModel = new EmailBodyViewModel
            {
                Email   = contactModel.Email,
                Name    = contactModel.Name,
                Subject = contactModel.Subject,
                Message = contactModel.Message
            };

            //Instead of using stringbuilder to create email body, use an online source to create email body such as https://chamaileon.io/
            var emailBody = _renderMvcPartialsContact.RenderPartialViewToString("pvContactPartialViewTemplate", emailBodyModel);

            EmailParametersHelper emailModel = new EmailParametersHelper
            {
                BodyText = emailBody,
                EmailModelHelper = emailBodyModel
            };
            bool emailSentStatus = SendEmailHelper.SendEmail(emailModel);

            if (emailSentStatus)
            {
                TempData["Status"] = "Email Sent";
                return View();
            }

            TempData["Status"] = "Email Failed";
            return View();
        }
    }
}
