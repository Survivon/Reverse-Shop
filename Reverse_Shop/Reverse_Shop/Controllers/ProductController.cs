using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Model;
using Core.Classes.Product;
using Core.Classes.User;

namespace Reverse_Shop.Controllers
{
    public class ProductController : Controller
    {
        private static readonly ProductSaveOrUpdate ProductSaveOrUpdate = new ProductSaveOrUpdate();
        private static readonly ProductShow ProductShow = new ProductShow();
        private static readonly UserAccount UserAccount = new UserAccount();
        private int _selectProductId = 0;
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info(string name)
        {
            var model = ProductShow.ProductInfo(name);
            var httpCookie = Request.Cookies["login"];
            if (httpCookie != null)
            {
                int id = UserAccount.UserInfo(httpCookie.Value).Id;
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
            if (ProductSaveOrUpdate.ProductCoast(productId, coast))
            {
                var httpCookie = Request.Cookies["login"];
                if (httpCookie != null)
                    ProductSaveOrUpdate.AddNewBuyer(productId, UserAccount.UserInfo(httpCookie.Value).Id, coast);
                ViewBag.Check = true;
                return Redirect("/" + ProductShow.ProductInfo(productId).Name.TrimEnd(' '));

            }
            ViewBag.Check = false;
            return Redirect("/" + ProductShow.ProductInfo(productId).Name.TrimEnd(' '));
        }

        public PartialViewResult EditProductView(string productName)
        {
            var model = ProductShow.ProductInfo(productName);
            return PartialView(model);
        }

        public ActionResult EditProduct(Product product)
        {
            ProductSaveOrUpdate.UpdateProduct(product);
            return RedirectToAction("UserInfo", "User");
        }

        public ActionResult DeleteProduct(int productId)
        {
            ProductSaveOrUpdate.DeleteProduct(productId);
            return RedirectToAction("UserInfo", "User");
        }
    }
}