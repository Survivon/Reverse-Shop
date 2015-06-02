using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Reverse_Shop.Models;
using Core;
using Core.Model;


namespace Reverse_Shop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserWorker _userWorker = new UserWorker();
        private readonly ProductWorker _productWorker = new ProductWorker();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FastRegistration(PartRegistrationModel model)
        {
            bool registrationSeccess= _userWorker.RegistrationAccount(model.Email);
            return View("../User/Mail");
        }

        [HttpPost]
        public ActionResult FullRegistration(FullRegistrationModel model)
        {
            Core.Model.User createUser = new User();
            createUser.Email = model.Email;
            createUser.FirstName = model.FirstName;
            createUser.SecondName = model.SecondName;
            createUser.Phone = model.Phone;
            bool registrationSuccess = _userWorker.RegistrationAccount(createUser);
            return View("../User/Mail");
        }

        public ActionResult Active(string login)
        {
            var user = _userWorker.ActivateAccount(login);
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
                Core.Model.User user = _userWorker.UserInfo(httpCookie.Value);
                user.PasswordHash = password.Password;
                _userWorker.UpdateUserInfo(user);
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
                var user = _userWorker.UserInfo(httpCookie.Value);
                ViewBag.Name = user.FirstName.Trim(' ') + " " + user.SecondName;
            }
            return View();
        }

        public ActionResult UserExit()
        {
            var httpCookie = Request.Cookies["login"];
            if (httpCookie != null) httpCookie.Value = null;
            return View("../Home/Index");
        }

        public PartialViewResult LogInView()
        {
            return PartialView();
        }

        public ActionResult LogInUser(LogInModel login)
        {
            if (login.Active)
            {
                if (_userWorker.CheckUser(login.Email, login.Password))
                {
                    if (Request.Cookies["login"] == null)
                    {
                        HttpCookie newCookie = new HttpCookie("login");
                        newCookie.Value = _userWorker.AutorizationUser(login.Email).LoginHash;
                        newCookie.Expires = DateTime.Now.AddDays(7);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Request.Cookies["login"].Value = _userWorker.AutorizationUser(login.Email).LoginHash;
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
                    newCookie.Value = _userWorker.AutorizationUser(login.Email).LoginHash;
                    newCookie.Expires = DateTime.Now.AddHours(1);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Request.Cookies["login"].Value = _userWorker.AutorizationUser(login.Email).LoginHash;
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
                var user = _userWorker.UserInfo(httpCookie.Value);
                ViewBag.Name = user.FirstName + " " + user.SecondName;
                model = user;
            }
            return PartialView(model);
        }

        public ActionResult UpdateUser(User user)
        {
            if (_userWorker.UpdateUserInfo(user))
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

        public PartialViewResult ChangePassword(PasswordModel password)
        {
            var httpCookie = Request.Cookies["login"];
            if (httpCookie != null)
            {
                var user = _userWorker.UserInfo(httpCookie.Value);
                user.PasswordHash = password.Password;
                if (_userWorker.UpdatePassword(user))
                {
                    ViewBag.Check = true;
                }
                else ViewBag.Check = false;
            }
            return ChangePasswordView();
        }


        public PartialViewResult BuyListPartialView()
        {
            var httpCookie = Request.Cookies["login"];
            var model = new List<Product>();
            if (httpCookie != null)
            {
               model = _productWorker.UsersProductsList(httpCookie.Value);
            }
            return PartialView(model);
        }
    }
}