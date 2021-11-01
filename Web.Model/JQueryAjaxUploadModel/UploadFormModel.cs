using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Web.Model.JQueryAjaxUploadModel
{
    public class UploadFormModel
    {
        [Display(Name="First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string Firstname             { get; set; }

        [Display(Name = "Select Image")]
        [Required(ErrorMessage = "Image Required")]
        public HttpPostedFileBase UploadImage { get; set; }
    }
}
