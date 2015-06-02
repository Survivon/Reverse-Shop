using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Infrastructure;
using Infrastructure.Classes;
using Infrastructure.Interface;
using System.Net.Mail;
using System.Security.Cryptography;

namespace Core
{
    public class UserWorker
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly HashWorker _hashWorker = new HashWorker();
        private readonly IProductRepository _productRepository = new ProductRepository();
        private readonly ProductWorker _productWorker = new ProductWorker();


        #region ShowInfo

        public bool CheckUser(string loginHash,string passwordHash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                loginHash = _hashWorker.GetMd5Hash(md5Hash, loginHash);
                passwordHash = _hashWorker.GetMd5Hash(md5Hash, passwordHash);
            }
            return _userRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash&& u.PasswordHash==passwordHash) != null;
        }

        public User AutorizationUser(string email)
        {
            var user = new User();
            var dbUser = _userRepository.Users().FirstOrDefault(u => u.Email == email);
            if (dbUser != null) user.LoginHash = dbUser.LoginHash;
            return user;
        }

        public User UserInfo(string loginHash)
        {
            var newUser = new User();
            var userInfo = _userRepository.Users().FirstOrDefault(u=>u.LoginHash==loginHash);
            if (userInfo != null)
            {
                newUser.Account = userInfo.Account;
                newUser.Active = userInfo.Active;
                newUser.Email = userInfo.Email;
                newUser.FirstName = userInfo.FirstName;
                newUser.Id = userInfo.Id;
                newUser.LoginHash = userInfo.LoginHash;
                newUser.Phone = userInfo.Phone;
                newUser.SecondName = userInfo.SecondName;
            }
            else newUser = null;
            return newUser;
        }

        public List<Product> UsersListofProducts(string loginHash)
        {
            var dbListOfProduct = _productRepository.Products().Where(p =>
            {
                var firstOrDefault = _userRepository.Users().FirstOrDefault(u=>u.LoginHash==loginHash);
                return firstOrDefault != null && p.IdSaler == firstOrDefault.Id;
            });
            var productList = new List<Product>();
            foreach (var item in dbListOfProduct)
            {
                var productItem = new Product()
                {
                    Category = item.Category,
                    Coast = item.Coast,
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    Info = item.Info,
                    Time = _productWorker.TimeValueProduct(item.Id)
                };
                productList.Add(productItem);
            }
            return productList;
        } 
        

        #endregion


        #region SaveOrUpdate

        private static string MailBody(string loginHash)
        {
            string body = string.Format("<h2>Thank you for registration on ReverseShop!</h2><br>" +
                "<h4>To confirm registration, click on the link below:</h4><br><a href={0}{1}\">Click here!</a>",
                "\"localhost:52714/Activate?login=", loginHash);
            return body;
        }
        
        public bool RegistrationAccount(string email)
        {
            var loginHash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                loginHash = _hashWorker.GetMd5Hash(md5Hash, email);
            }
            var user = new Infrastructure.Model.User { Email = email, 
                Active = false,
                LoginHash = loginHash,
                Password="",
                PasswordHash="",
                Account="base",
                FirstName="",
                SecondName="",
                Phone=0 };
            _userRepository.Save(user);
            //Core.MailSender.SendMail(user.Email,"Activate Account",MailBody(user.LoginHash),null);
            return true;
        }

        public bool RegistrationAccount(User user)
        {
            var loginHash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                loginHash = _hashWorker.GetMd5Hash(md5Hash, user.Email);
            }
            var newuser = new Infrastructure.Model.User
            {
                Active = false,
                LoginHash = loginHash,
                Password = "",
                PasswordHash = "",
                Account = "base",
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Phone = user.Phone
            };
            _userRepository.Save(newuser);
           // Core.MailSender.SendMail(user.Email, "Activate Account", MailBody(user.LoginHash), null);
            return true;
        }

        public User ActivateAccount(string loginHash)
        {
            var dbUser = _userRepository.Users().FirstOrDefault(u => u.LoginHash.Trim(' ') == loginHash);
            var coreUser = new User();
            if (dbUser == null) return coreUser;
            dbUser.Active = true;
            _userRepository.Update(dbUser);
            coreUser = new User()
            {
                FirstName = dbUser.FirstName,
                SecondName=dbUser.SecondName,
                Id=dbUser.Id,
                Phone=dbUser.Phone,
                LoginHash=dbUser.LoginHash
            };
            return coreUser;
        }

        public bool UpdateUserInfo(User user)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                user.PasswordHash = _hashWorker.GetMd5Hash(md5Hash, user.PasswordHash);
            }
            var newUser = new Infrastructure.Model.User
            {
                FirstName=user.FirstName,
                SecondName=user.SecondName,
                Active=true,
                PasswordHash=user.PasswordHash,
                Phone=user.Phone,
                Role=false
            };
            try
            {
                _userRepository.Update(newUser);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool UpdatePassword(User user)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                user.PasswordHash = _hashWorker.GetMd5Hash(md5Hash, user.PasswordHash);
            }
            var dbUser = _userRepository.SearchUser(user.Id);
            if (dbUser.PasswordHash == user.PasswordHash)
            {
                dbUser.PasswordHash = user.PasswordHash;
                try
                {
                    _userRepository.Update(dbUser);
                    return true;
                }
                catch (Exception exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Delete

        public bool DeleteUser(string loginHash)
        {
            try
            {
                _userRepository.DeleteUser(_userRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash));
                return true;
            }
            catch (Exception exception)
            {

                return false;
            }
        }

        #endregion
    }
}
