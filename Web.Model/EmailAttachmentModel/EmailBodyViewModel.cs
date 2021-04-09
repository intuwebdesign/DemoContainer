using System.ComponentModel.DataAnnotations;

namespace Web.Model.EmailAttachmentModel
{
    public class EmailBodyViewModel
    {
        [Required(ErrorMessage = "Email Required")]
        public string Email { get;set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
    }

    public class EmailParametersHelper
    {
        public string BodyText                      { get; set; }
        public EmailBodyViewModel EmailModelHelper  { get; set; }
    }
}
