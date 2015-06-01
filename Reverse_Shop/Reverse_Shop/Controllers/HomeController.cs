using System;
using System.Collections.Generic;
using System.Data;
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
        private int _itemInPage = 10;
        public ActionResult Index()
        {

            HttpCookie newCookie = new HttpCookie("login");
            newCookie.Value = "";
            newCookie.Expires = DateTime.Now.AddDays(7);

            var model = _productWorker.ProductInPage(1, _itemInPage).ToList();
            ViewBag.List = model;
            int count = _productWorker.PageOfProductCount(_itemInPage);
            ViewBag.count = count;
            ViewBag.pageCounter = PageList(1);
            return View(model);
        }

        public ActionResult FastRegistration(bool mode=false)
        {
            if (mode)
            {
                
                return View();
            }
            else
            {
                return View("Registration");
            }
            
        }

        public ActionResult Search(string productName)
        {
            var product = _productWorker.SearchProducts(productName);
            if (!Request.IsAjaxRequest()) return View(product);
            ViewBag.pageCounter = PageList(1);
            return PartialView(product);
        }

        public ActionResult Page(int page)
        {
            var model = _productWorker.ProductInPage(page, _itemInPage).ToList();
            ViewBag.List = model;
            ViewBag.pageCounter = PageList(page);
            return PartialView(model);
        }

        private List<object> PageList(int page)
        {
            int countPages = _productWorker.PageOfProductCount(_itemInPage);
            var pageLisrAllItem = new List<object>();
            if (countPages <= 5)
            {
                for (int i = 1; i <= countPages; i++)
                {
                    pageLisrAllItem.Add(i);
                }
                return pageLisrAllItem;
            }
            if (page < 5&&countPages-page-5<=0)
            {
                for (int i = 1; i <= countPages; i++)
                {
                    pageLisrAllItem.Add(i);
                }
                return pageLisrAllItem;
            }
            if (page<5&&countPages-page-5>0)
            {
                for (int i = 1; i < (page+2<=5?5:page+2); i++)
                {
                    pageLisrAllItem.Add(i);
                }
                pageLisrAllItem.Add("...");
                pageLisrAllItem.Add(countPages-1);
                pageLisrAllItem.Add(countPages);
                pageLisrAllItem = pageLisrAllItem.Distinct().ToList();
                return pageLisrAllItem;
            }
            if (page>=5&&countPages-page-5>=0)
            {
               pageLisrAllItem.Add(1);
                pageLisrAllItem.Add(2);
                pageLisrAllItem.Add("...");
                for (int i = page - 2; i <= page + 2; i++)
                {
                    pageLisrAllItem.Add(i);
                }
                pageLisrAllItem.Add("..,");
                pageLisrAllItem.Add(countPages - 1);
                pageLisrAllItem.Add(countPages);
                pageLisrAllItem = pageLisrAllItem.Distinct().ToList();
                return pageLisrAllItem;
            }
            if (page >= 5  && countPages - page - 5 < 0)
            {
                pageLisrAllItem.Add(1);
                pageLisrAllItem.Add(2);
               pageLisrAllItem.Add("...");
                for (int i = countPages - 5; i <= countPages; i++)
                {
                    pageLisrAllItem.Add(i);
                }
                pageLisrAllItem = pageLisrAllItem.Distinct().ToList();
                return pageLisrAllItem;
            }
            return pageLisrAllItem;
        } 
    }
}