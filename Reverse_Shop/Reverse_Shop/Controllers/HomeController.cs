using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reverse_Shop.Models;
using Core;

namespace Reverse_Shop.Controllers
{
    public class HomeController : Controller
    {
        private UserWorker userWorker = new UserWorker();
        public ActionResult Index()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            var product = userWorker.AllProducts();
            ViewBag.product = product;
            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}