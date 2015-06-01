using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;
using Infrastructure.Interface;

namespace Infrastructure.Classes
{
    public class UserRepository:IUserRepository
    {
        private readonly ReverseShopContext _dbContext = new ReverseShopContext();
        public void Save(User user) 
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
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
