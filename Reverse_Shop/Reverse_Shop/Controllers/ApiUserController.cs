using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Classes.User;
using Core.Model;

namespace Reverse_Shop.Controllers
{
    public class ApiUserController : ApiController
    {
        private static readonly UserAccount UserAccount = new UserAccount();
        private static readonly UserRegistration UserRegistration = new UserRegistration();
        private static readonly UsersProduct UsersProduct = new UsersProduct();


        public User CreateUser(User user)
        {
            UserRegistration.RegistrationAccount(user);
            return UserAccount.UserInfo(UserRegistration.AutorizationUser(user.Email).LoginHash);
        }

        public User GetUser(string loginHash)
        {
            return UserAccount.UserInfo(loginHash);
        }

        public User Autorization(string loginHash, string passwordHash)
        {
            if (UserRegistration.CheckUser(loginHash, passwordHash))
            {
                return UserAccount.UserInfo(loginHash);
            }
            else
            {
                return null;
            }
        }

        public User Edit(User user)
        {
            var userSavety = UserAccount.UserInfo(user.LoginHash);
            if (user.PasswordHash != userSavety.PasswordHash)
            {
                UserAccount.UpdatePassword(user);
            }
            if (user.FirstName != userSavety.FirstName || user.SecondName != userSavety.SecondName ||
                user.Phone != userSavety.Phone)
            {
                UserAccount.UpdateUserInfo(user);
            }
            return UserAccount.UserInfo(user.LoginHash);
        }

        public IEnumerable<Product> SaleUsersProducts(string loginHash)
        {
            return UsersProduct.UsersSaleProductsList(loginHash);
        }

        public IEnumerable<Product> BuyUsersProducts(string loginHash)
        {
            return UsersProduct.UsersListofProducts(loginHash);
        } 
    }
}
