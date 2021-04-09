using System;
using System.Net;
using System.Net.Mail;
using System.Web;
using Web.Model.EmailAttachmentModel;

namespace Web.Controller.Helpers
{
    public static class SendEmailHelper
    {
        public static bool SendEmail(EmailParametersHelper model)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(address: model.EmailModelHelper.Email, displayName: model.EmailModelHelper.Name));
            mailMessage.From = new MailAddress(address:"YourSMTPSettings@myemail.com", displayName: "Company Name");
            mailMessage.Subject = model.EmailModelHelper.Subject;
            mailMessage.Body = model.BodyText;

            var pathToAttachment = HttpContext.Current.Server.MapPath("~/UploadedFiles/upload.zip");
            Attachment attachment = new Attachment(pathToAttachment)
            {
                Name = "Upload.zip"
            };
            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;

            bool isDebuggerEnabled = HttpContext.Current.IsDebuggingEnabled;

            if (isDebuggerEnabled)
            {
                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "smtpUsername",
                            Password = "smtpPassword"
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "sMtpHost";
                        smtp.Port = Convert.ToInt32(90);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtp.PickupDirectoryLocation = @"W:\EmailPickUp\";//Change to your own location
                        smtp.EnableSsl = false;

                        smtp.Send(mailMessage);
                        return true;

                    }
                }
                catch (SmtpFailedRecipientException e)
                {
                    throw new ApplicationException($"Failed to send email {e}");
                }
                catch (SmtpException e)
                {
                    throw new ApplicationException($"Failed to send email {e}");
                }
                catch (Exception e)
                {
                    throw new ApplicationException($"Failed to send email {e}");
                }

            }

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    var credential = new NetworkCredential
                    {
                        UserName = "smtpUsername",
                        Password = "smtpPassword"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "sMtpHost";
                    smtp.Port = Convert.ToInt32("sMtpPort");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = Convert.ToBoolean("sMtpRequiresSll");


                    smtp.Send(mailMessage);
                    return true;
                }
            }
            catch (SmtpFailedRecipientException e)
            {
                throw new ApplicationException($"Failed to send email {e}");
            }
            catch (SmtpException e)
            {
                throw new ApplicationException($"Failed to send email {e}");
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Failed to send email {e}");
            }
        }
    }
}
