using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Classes;
using Infrastructure.Interface;

namespace Core.Classes.User
{
    public class UserRegistration
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly HashWorker _hashWorker = new HashWorker();

        public bool CheckUser(string loginHash, string passwordHash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                loginHash = _hashWorker.GetMd5Hash(md5Hash, loginHash);
                passwordHash = _hashWorker.GetMd5Hash(md5Hash, passwordHash);
            }
            return _userRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash && u.PasswordHash == passwordHash) != null;
        }

        public bool RegistrationAccount(string email)
        {
            var loginHash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                loginHash = _hashWorker.GetMd5Hash(md5Hash, email);
            }
            var user = new Infrastructure.Model.User
            {
                Email = email,
                Active = false,
                LoginHash = loginHash,
                Password = "",
                PasswordHash = "",
                Account = "base",
                FirstName = "",
                SecondName = "",
                Phone = 0
            };
            _userRepository.Save(user);
            //Core.MailSender.SendMail(user.Email,"Activate Account",MailBody(user.LoginHash),null);
            return true;
        }

        public bool RegistrationAccount(Model.User user)
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

        public Model.User AutorizationUser(string email)
        {
            var user = new Model.User();
            var dbUser = _userRepository.Users().FirstOrDefault(u => u.Email == email);
            if (dbUser != null) user.LoginHash = dbUser.LoginHash;
            return user;
        }
    }
}
