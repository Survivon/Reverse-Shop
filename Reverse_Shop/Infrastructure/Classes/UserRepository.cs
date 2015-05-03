using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;
using Infrastructure.Interface;

namespace Infrastructure.Classes
{
    public class UserRepository:IUserRepository
    {
        private ReverseShopContext _dbContext = new ReverseShopContext();
        public void SaveOrUpdate(User user) 
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User SearchUser(int id) 
        {
            return _dbContext.Users.Find(id);
        }

        public IEnumerable<User> Users() 
        {
            return _dbContext.Users.ToList();
        }

        public void DeleteUser(int id) 
        {
            _dbContext.Users.Remove(_dbContext.Users.Find(id));
        }

        public void DeleteUser(User user) 
        {
            _dbContext.Users.Remove(user);
        }
    }
}
