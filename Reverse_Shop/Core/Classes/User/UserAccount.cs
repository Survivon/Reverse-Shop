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
    public class UserAccount
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly HashWorker _hashWorker = new HashWorker();

        public Model.User UserInfo(string loginHash)
        {
            var newUser = new Model.User();
            var userInfo = _userRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash);
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

        public Model.User ActivateAccount(string loginHash)
        {
            var dbUser = _userRepository.Users().FirstOrDefault(u => u.LoginHash.Trim(' ') == loginHash);
            var coreUser = new Model.User();
            if (dbUser == null) return coreUser;
            dbUser.Active = true;
            _userRepository.Update(dbUser);
            coreUser = new Model.User()
            {
                FirstName = dbUser.FirstName,
                SecondName = dbUser.SecondName,
                Id = dbUser.Id,
                Phone = dbUser.Phone,
                LoginHash = dbUser.LoginHash
            };
            return coreUser;
        }

        public bool UpdateUserInfo(Model.User user)
        {
            if (user.PasswordHash != null)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    user.PasswordHash = _hashWorker.GetMd5Hash(md5Hash, user.PasswordHash);
                }
            }
            var newUser = _userRepository.SearchUser(user.Id);
            if (user.FirstName != null)
            {
                if (user.FirstName != newUser.FirstName)
                    newUser.FirstName = user.FirstName;
            }
            if (user.SecondName != null)
            {
                if (user.SecondName != newUser.SecondName)
                    newUser.SecondName = user.SecondName;
            }
            if (user.Phone != null)
            {
                if (user.Phone != newUser.Phone)
                    newUser.Phone = user.Phone;
            }
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

        public bool UpdatePassword(Model.User user)
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
    }
}
