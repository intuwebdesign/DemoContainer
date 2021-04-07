using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Web.Controller.Helpers;

namespace Web.Controller.Controllers
{
    public class BulkController : System.Web.Mvc.Controller
    {

        [HttpGet]
        public ActionResult Home()
        {
            ViewBag.Message = "Bulk.";

            return View();
        }

        [HttpPost]
        public ActionResult Home(HttpPostedFileBase file)
        {

            try
            {
                string mimeType = MimeMapping.GetMimeMapping(file.FileName);

                if (mimeType == "application/x-zip-compressed")
                {
                    if (file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                        file.SaveAs(path);

                        IExtractAndBulkInsert iExtractAndBulkInsert = new ExtractAndBulkInsert();
                        bool status = iExtractAndBulkInsert.ReadExcelFile(path);

                        if (status)
                        {
                            ViewBag.Message = "Insert Completed.";
                            return View();
                        }
                        
                    }
                }
                ViewBag.Message = "Insert failed.";
                return View();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
