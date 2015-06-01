using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool? Role { get; set; }

        public string Account { get; set; }

        public bool Active { get; set; }

        public int Phone { get; set; }

        public string LoginHash { get; set; }

        public string PasswordHash { get; set; }

    }
}
