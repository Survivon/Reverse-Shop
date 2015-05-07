﻿using System;
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
       
    }
}