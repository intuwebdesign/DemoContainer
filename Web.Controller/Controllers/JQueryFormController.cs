using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Web.Model.JQueryAjaxUploadModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace Web.Controller.Controllers
{
    public class JQueryFormController  : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            ViewBag.Message = "JQuery Ajax Form.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload(UploadFormModel model)
        {
            if (!ModelState.IsValid)
            {
                //You would actually return the errors allowing the user to correct them
                return Json(new { Confirm = false, isJsonErrorList = true, message = $"{model.Firstname} Form Fields Not Valid" }, JsonRequestBehavior.AllowGet);
            }
            var files = Request.Files;
            HttpPostedFileBase image = files[0];

            if (image != null && image.ContentLength > 0)
            {
                //Here you would do whatever validation you require

                bool isImageValid = IsFileTypeValid(image);

                if (isImageValid)
                {
                    //Either send as attachemnt or save to disk
                    //Then return message to user
                    return Json(new { Confirm = true, isJsonErrorList = false, message = $"{model.Firstname} Image Uploaded" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Confirm = true, isJsonErrorList = false, message = $"{model.Firstname} Image Upload failed" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Confirm = false, isJsonErrorList = true, message = $"{model.Firstname} Image Upload failed" }, JsonRequestBehavior.AllowGet);
        }

        private bool IsFileTypeValid(HttpPostedFileBase file)
        {
            bool isValid = false;
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    if (IsImageValid(img.RawFormat))
                    {
                        isValid = true;
                    }
                }
            }
            catch (Exception e)
            {
                //Not Valid
            }

            return isValid;
        }

        private bool IsImageValid(ImageFormat rawFormat)
        {
            List<ImageFormat> formats = ValidFormats();

            foreach (ImageFormat format in formats)
            {
                if (rawFormat.Equals(format))
                {
                    return true;
                }
            }

            return false;
        }

        private List<ImageFormat> ValidFormats()
        {
            List<ImageFormat> formats = new List<ImageFormat>
            {
                ImageFormat.Jpeg,
                ImageFormat.Png
            };

            return formats;
        }
    }
}
