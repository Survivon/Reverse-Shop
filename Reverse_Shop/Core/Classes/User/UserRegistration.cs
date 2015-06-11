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

        public bool RegistrationAccount(Model.User user)
        {
            var loginHash = "";
            var passwordHash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                loginHash = _hashWorker.GetMd5Hash(md5Hash, user.Email);
                passwordHash = _hashWorker.GetMd5Hash(md5Hash, user.PasswordHash);
            }
            var newuser = new Infrastructure.Model.User
            {
                Active = true,
                LoginHash = loginHash,
                Password = "",
                PasswordHash = passwordHash,
                Account = "base",
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Phone = user.Phone,
                Email=user.Email
            };
            _userRepository.Save(newuser);
            return true;
        }

        public Model.User AutorizationUser(string email)
        {
            var user = new Model.User();
            var dbUser = _userRepository.Users().FirstOrDefault(u => u.Email.Trim(' ') == email);
            if (dbUser != null) user.LoginHash = dbUser.LoginHash;
            return user;
        }
    }
}
