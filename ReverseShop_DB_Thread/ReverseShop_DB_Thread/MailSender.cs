using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ReverseShop_DB_Thread
{
    public class MailSender
    {
        public static void SendMail(string mailto, string caption, string message, string attachFile = null)
        {
            const string email = "eremenkdima1995@gmail.com";
            try
            {
                MailMessage mail = new MailMessage {From = new MailAddress(email)};
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                mail.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(email.Split('@')[0], "Survivon12345"),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
    }

}
