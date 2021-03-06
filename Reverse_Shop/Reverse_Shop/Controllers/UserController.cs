﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Reverse_Shop.Models;
using Core;
using Core.Model;
using System.IO;
using Core.Classes.User;
using Core.Classes.Product;


namespace Reverse_Shop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRegistration _userRegistration = new UserRegistration();
        private readonly UserAccount _userAccount = new UserAccount();
        private readonly ProductShow _productList = new ProductShow();
        private readonly UsersProduct _usersProduct = new UsersProduct();
        private readonly ProductSaveOrUpdate _productSave = new ProductSaveOrUpdate();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FullRegistration(FullRegistrationModel model)
        {
            Core.Model.User createUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Phone = model.Phone,
                PasswordHash = model.Password
            };
            bool registrationSuccess = _userRegistration.RegistrationAccount(createUser);
            if (registrationSuccess)
            {
                var httpCookie = Request.Cookies["login"];
                if (httpCookie == null)
                {
                    HttpCookie newCookie = new HttpCookie("login");
                    newCookie.Value = _userRegistration.AutorizationUser(model.Email).LoginHash;
                    newCookie.Expires = DateTime.Now.AddDays(7d);
                    Request.Cookies.Add(newCookie);
                    var cookie = new HttpCookie("login")
                    {
                        Value = _userRegistration.AutorizationUser(model.Email).LoginHash,
                        Expires = DateTime.Now.AddMinutes(10),
                    };
                    Response.SetCookie(cookie);
                    Response.Cookies["login"].Value = _userRegistration.AutorizationUser(model.Email).LoginHash;
                    Response.Cookies["login"].Expires = DateTime.Now.AddDays(7d);
                   
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Active(string login)
        {
            var user = _userAccount.ActivateAccount(login);
            HttpCookie newCookie = new HttpCookie("login");
            newCookie.Value = user.LoginHash;
            newCookie.Expires = DateTime.Now.AddDays(7);
            return View();
        }

        public ActionResult Save(PasswordModel password)
        {
            var httpCookie = Request.Cookies["login"];
            if (httpCookie != null)
            {
                Core.Model.User user = _userAccount.UserInfo(httpCookie.Value);
                user.PasswordHash = password.Password;
                _userAccount.UpdateUserInfo(user);
            }
            return View("../Home/Index");
        }

        [HttpGet]
        public ActionResult UserInfo()
        {
            var httpCookie = Request.Cookies["login"];
            var model = new User();
            if (httpCookie != null)
            {
                var user = _userAccount.UserInfo(httpCookie.Value);
                ViewBag.Name = user.FirstName.Trim(' ') + " " + user.SecondName;
            }
            return View();
        }

        public ActionResult UserExit()
        {
            HttpCookie myCookie = new HttpCookie("login");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Index","Home");
        }

        public PartialViewResult LogInView()
        {
            return PartialView();
        }

        public ActionResult LogInUser(LogInModel login)
        {
            if (login.Active)
            {
                if (_userRegistration.CheckUser(login.Email, login.Password))
                {
                    if (Request.Cookies["login"] == null)
                    {
                        HttpCookie newCookie = new HttpCookie("login");
                        newCookie.Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                        newCookie.Expires = DateTime.Now.AddDays(7d);
                        Request.Cookies.Add(newCookie);
                        var cookie = new HttpCookie("login")
                        {
                            Value = _userRegistration.AutorizationUser(login.Email).LoginHash,
                            Expires = DateTime.Now.AddMinutes(10),
                        };
                        Response.SetCookie(cookie);
                        Response.Cookies["login"].Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                        Response.Cookies["login"].Expires = DateTime.Now.AddDays(7d);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Request.Cookies["login"].Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.LogIn = false;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                if (Request.Cookies["login"] == null)
                {
                    HttpCookie newCookie = new HttpCookie("login");
                    newCookie.Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                    newCookie.Expires = DateTime.Now.AddDays(7d);
                    Request.Cookies.Add(newCookie);
                    var cookie = new HttpCookie("login")
                    {
                        Value = _userRegistration.AutorizationUser(login.Email).LoginHash,
                        Expires = DateTime.Now.AddMinutes(10),
                    };
                    Response.SetCookie(cookie);
                    Response.Cookies["login"].Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                    Response.Cookies["login"].Expires = DateTime.Now.AddDays(1d);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpCookie newCookie = new HttpCookie("login");
                    newCookie.Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                    newCookie.Expires = DateTime.Now.AddDays(7d);
                    Request.Cookies.Add(newCookie);
                    var cookie = new HttpCookie("login")
                    {
                        Value = _userRegistration.AutorizationUser(login.Email).LoginHash,
                        Expires = DateTime.Now.AddMinutes(10),
                    };
                    Response.SetCookie(cookie);
                    Request.Cookies["login"].Value = _userRegistration.AutorizationUser(login.Email).LoginHash;
                    Request.Cookies["login"].Expires = DateTime.Now.AddHours(1);
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public PartialViewResult UserInformation()
        {
            var httpCookie = Request.Cookies["login"];
            var model = new User();
            if (httpCookie != null)
            {
                var user = _userAccount.UserInfo(httpCookie.Value);
                ViewBag.Name = user.FirstName + " " + user.SecondName;
                model = user;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            if (_userAccount.UpdateUserInfo(user))
            {
                ViewBag.Check = true;
            }
            else
            {
                ViewBag.Check = false;
            }
            return RedirectToAction("UserInfo");
        }

        public PartialViewResult ChangePasswordView()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ChangePassword(PasswordModel password)
        {
            var httpCookie = Request.Cookies["login"];
            if (httpCookie != null)
            {
                var user = _userAccount.UserInfo(httpCookie.Value);
                if (_userAccount.CheckOldPassword(user.LoginHash, password.OldPassword))
                {
                    user.PasswordHash = password.Password;
                    ViewBag.Check = _userAccount.UpdatePassword(user);
                }
            }
            return RedirectToAction("UserInfo","User");
        }


        public PartialViewResult BuyListPartialView()
        {
            var httpCookie = Request.Cookies["login"];
            var model = new List<Product>();
            if (httpCookie != null)
            {
               model = _productList.UsersProductsList(httpCookie.Value);
            }
            return PartialView(model);
        }

        public PartialViewResult SaleListPartialView()
        {
            var httpCookie = Request.Cookies["login"];
            var model = new List<Product>();
            if (httpCookie != null)
            {
                model = _usersProduct.UsersSaleProductsList(httpCookie.Value);
            }
            return PartialView(model);
        }

        public PartialViewResult CreateProductPartialView()
        {
            return PartialView();
        }

        public PartialViewResult ProductSavePartialView(ProductModel product)
        {
            var productCore = new Core.Model.SaveProduct
            {
                Name = product.Name,
                Info = product.Info,
                IdSaler = _userAccount.UserInfo(Request.Cookies["login"].Value).Id,
                Category = product.Category,
                Time = product.Time
            };
            if (_productSave.SaveProduct(productCore))
            {
                var firstOrDefault = _productList.SearchProducts(product.Name).FirstOrDefault();
                if (firstOrDefault != null)
                    ViewBag.ProductId = firstOrDefault.Name;
                return PartialView();
            }
            else
            {
                return CreateProductPartialView();
            }
        }

        public ActionResult UploadFile(HttpPostedFileBase file, string productName)
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                file.SaveAs(path);
                var productCore = _productList.SearchProducts(productName).FirstOrDefault();
                productCore.Image = "/Content/Image/" + fileName;
                _productSave.UpdateProduct(productCore);
            }
            return RedirectToAction("UserInfo");
        }


    }
}