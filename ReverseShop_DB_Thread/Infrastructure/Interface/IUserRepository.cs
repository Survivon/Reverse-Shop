using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;

namespace Infrastructure.Interface
{
    public interface IUserRepository
    {
        void SaveOrUpdate(User user);

        User SearchUser(int id);

        IEnumerable<User> Users();

        void DeleteUser(int id);

        void DeleteUser(User user);
    }
}
