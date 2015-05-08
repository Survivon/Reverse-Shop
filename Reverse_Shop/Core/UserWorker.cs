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

namespace Core
{
    public class UserWorker
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        #region ShowInfo

        public bool CheckUser(string userHash)
        {
            return _userRepository.Users().FirstOrDefault(u => u.LoginHash == userHash) != null;
        }

        public User UserInfo(int userId)
        {
            var newUser = new User();
            var userInfo = _userRepository.SearchUser(userId);
            if (userInfo != null)
            {
                newUser.Account = userInfo.Account;
                newUser.Active = userInfo.Active;
                newUser.Email = userInfo.Email;
                newUser.FirstName = userInfo.FirstName;
                newUser.Id = userInfo.Id;
                newUser.LoginHash = userInfo.LoginHash;
                newUser.Phone = userInfo.Phone;
                newUser.SecondName = newUser.SecondName;
            }
            else newUser = null;
            return newUser;
        }

        public bool ActivateAccount(string email)
        {
            var user = new Infrastructure.Model.User {Email = email};
            _userRepository.SaveOrUpdate(user);
            
            return true;
        }

        //public bool ActivateAccount(User user)
        //{
            
        //}

        #endregion


        #region SaveOrUpdate
    

        #endregion

        #region Delete

        #endregion
    }
}
