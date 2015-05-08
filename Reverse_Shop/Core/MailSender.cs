using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace Core
{
    public class MailSender
    {
        public void SendMessage(string strName, string strLog, string strText, string strPass, string outMail,
                        string strSubject)
        {
            try
            {
                // обнулим на всяк случай
                string smtp = "";
                int port = 0;

                // здесь не уверен на счет портов
                // может (скорее всего) они неправильны, но если что 
                // это все легко гуглится ;) парвильный только gmail.com
                var mailCoding = new Dictionary<string, int>
            {
                {"gmail.com", 587},
                {"yandex.ru", 225},
                {"mail.ru", 235},
                {"list.ru", 254},
                {"inbox.ru", 215},
                {"bk.ru", 255}
            };

                // поиск нужного порта и smtp при отправке
                // можно так сделать, но нет мы КРУТЫЕ
                // и напишем с помощью LINQ
                //foreach (var kvp in mailCoding)
                //{
                //    if (strLog.IndexOf(kvp.Key, StringComparison.Ordinal) > -1)
                //    {
                //        smtp = "smtp." + kvp.Key;
                //        port = kvp.Value;
                //    }
                //}
                foreach (var kvp in mailCoding.Where(kvp => strLog.IndexOf(kvp.Key, StringComparison.Ordinal) > -1))
                {
                    smtp = "smtp." + kvp.Key;
                    port = kvp.Value;
                }

                using (var mailMessage = new MailMessage(strName + " <" + strLog + ">", outMail))
                {
                    mailMessage.Subject = strSubject; // тема письма
                    mailMessage.Body = strText; // письмо
                    mailMessage.IsBodyHtml = true; // без html, но можно включить
                    using (var sc = new SmtpClient(smtp, port))
                    {
                        sc.EnableSsl = true;
                        sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                        sc.UseDefaultCredentials = false;
                        sc.Credentials = new NetworkCredential(strLog, strPass);
                        sc.Send(mailMessage);
                    }
                }
            }
            catch (Exception exception)
            {
             //   MessageBox.Show(exception.Message, "Ошибка");
            }
        }

    }

}
