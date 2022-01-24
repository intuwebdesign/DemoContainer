using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Model.Html5CanvasModel
{
    public class Html5CanvasViewModel
    {
        public HttpPostedFileBase Image { get; set; }

        public string Base64Image { get; set; }
    }

    public static class CreateImageFromBase64
    {
        public static void CreateImage(string path,string base64String)
        {
            var matchGroups = Regex.Match(base64String, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
            var base64Data = matchGroups["data"].Value;

            byte[] bytes = Convert.FromBase64String(base64Data);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);
                image.Save(path, ImageFormat.Png);
            }
        }
    }
}
