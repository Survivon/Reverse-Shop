using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;

namespace Reverse_Shop.Controllers
{
    public class ProductController : Controller
    {
        private static readonly ProductWorker ProductWorker = new ProductWorker();
        private static readonly UserWorker UserWorker = new UserWorker();
        private int _selectProductId = 0;
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info(string name)
        {
            var model = ProductWorker.ProductInfo(name);
            var httpCookie = Request.Cookies["login"];
            if (httpCookie != null)
            {
                int id = UserWorker.UserInfo(httpCookie.Value).Id;
                if (model.BuyerId == id)
                {
                    ViewBag.Check = true;
                }
            }
            return View(model);
        }

        public PartialViewResult CoastPartialView(int productId)
        {
            ViewBag.Id = productId;
            _selectProductId = productId;
            return PartialView();
        }
        [HttpPost]
        public ActionResult NewProductCoast(decimal coast, int productId)
        {
            if (ProductWorker.ProductCoast(productId, coast))
            {
                var httpCookie = Request.Cookies["login"];
                if (httpCookie != null)
                    ProductWorker.AddNewBuyer(productId, UserWorker.UserInfo(httpCookie.Value).Id, coast);
                ViewBag.Check = true;
                return Redirect("/" + ProductWorker.ProductInfo(productId).Name.TrimEnd(' '));

            }
            ViewBag.Check = false;
            return Redirect("/" + ProductWorker.ProductInfo(productId).Name.TrimEnd(' '));
        }
    }
}