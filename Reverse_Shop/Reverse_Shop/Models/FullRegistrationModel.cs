using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reverse_Shop.Models
{
    public class FullRegistrationModel
    {
        
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public int Phone { get; set; }

    }
}