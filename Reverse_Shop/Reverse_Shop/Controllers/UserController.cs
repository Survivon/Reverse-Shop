using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Save(User user)
        {

            return View("../Home/Index");
        }
    }
}