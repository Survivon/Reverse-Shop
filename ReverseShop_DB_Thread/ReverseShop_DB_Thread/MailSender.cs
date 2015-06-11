using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;

namespace ReverseShop_DB_Thread
{
    public class MailSender
    {
        public static void SendMail(string mailto, string caption, string message, string attachFile = null)
        {

            const string username = "azure_44316051d1f2dbba96fe2865b48a0922@azure.com";
            const string pswd = "9YSj513NLGZ6ql0";
            var myMessage = new SendGridMessage();
            myMessage.AddTo(mailto);
            myMessage.From = new MailAddress("reverseshop2015@gmail.com", "Reverse Shop");
            myMessage.Subject = caption;
            myMessage.Text = message;

            // Create credentials, specifying your user name and password.

            var credentials = new NetworkCredential(username, pswd);
            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            // You can also use the **DeliverAsync** method, which returns an awaitable task.
#pragma warning disable 4014
            transportWeb.DeliverAsync(myMessage);
#pragma warning restore 4014

            
        }
    }

}
