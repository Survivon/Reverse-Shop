using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Model;

namespace Reverse_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductWorker _productWorker = new ProductWorker();
     //   private Core.ProductControllSystem _productControllSystem = new ProductControllSystem();
       
        public ActionResult Index()
        {
            var model = _productWorker.ProductInPage(1, 20).ToList();
            ViewBag.List = model;
            return View(model);
        }

        public ActionResult Search(string productName)
        {
            var product = _productWorker.SearchProducts(productName);
            if (!Request.IsAjaxRequest()) return View(product);
            return PartialView(product);
        }

        public ActionResult Page(int page)
        {
            var model = _productWorker.ProductInPage(page, 3).ToList();
            ViewBag.List = model;
            return PartialView(model);
        }
    }
}