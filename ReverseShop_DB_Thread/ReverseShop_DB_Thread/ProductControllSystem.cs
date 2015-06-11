using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Classes;
using Infrastructure.Interface;

namespace ReverseShop_DB_Thread
{
   public class ProductControllSystem
   {
       private static readonly IProductRepository ProductRepository = new ProductRepository();
       private static readonly ProductWorker ProductWorker = new ProductWorker();
       private static readonly IUserRepository UserRepository = new UserRepository();
       public ProductControllSystem()
       {
           Thread newProductControllerThread = new Thread(new ThreadStart(ControllFunction));
           newProductControllerThread.Start();
       }

       public static void ControllFunction()
       {
           while (true)
           {
               var productList = ProductRepository.Products();
               foreach (var item in productList)
               {
                   var time = ProductWorker.TimeValueProduct(item.Id);
                   if (time.Hours < 0 || time.Minutes < 0)
                   {
                       ReverseShop_DB_Thread.MailSender.SendMail(UserRepository.SearchUser(item.IdBuyer ?? 1).Email, "You Won!",
                           MailBody(item, true), null);
                       ReverseShop_DB_Thread.MailSender.SendMail(UserRepository.SearchUser(item.IdSaler).Email, "Your goods was bought!",
                           MailBody(item, false), null);
                       item.Active = false;
                       ProductRepository.SaveOrUpdate(item);
                   }
               }
           }
       }

       private static string MailBody(Infrastructure.Model.Product product, bool mode)
       {
           string mailBody = "";
           if (mode)
           {
               //change mail body
               mailBody = "<h2>You won an aukzion!</h2>" +
                          "<h3>Your goods " + product.Name + ".</h3>" +
                          "<p>You can phone to saler." +
                          "<h3>Name saler is"+UserRepository.SearchUser(product.IdSaler).FirstName+" "+
                          UserRepository.SearchUser(product.IdSaler).SecondName+"</h3>" +
                          " Phone number is +" +
                          UserRepository.SearchUser(product.IdSaler).Phone + "</p>" +
                          "<p>Product coast = " + product.Coast + "</p>";
               return mailBody;
           }
           mailBody = "<h2>Your goods was bought!</h2>" +
                      "<h3>Your goods " + product.Name + ".</h3>" +
                          "<h3>Name buyer is" + UserRepository.SearchUser(product.IdBuyer??1).FirstName + " " +
                          UserRepository.SearchUser(product.IdBuyer??1).SecondName + "</h3>" +
                          " Phone number is +" +
                          UserRepository.SearchUser(product.IdBuyer??1).Phone + "</p>" +
                          "<p>Product coast = " + product.Coast + "</p>";
           return mailBody;
       }

       
   }
}
