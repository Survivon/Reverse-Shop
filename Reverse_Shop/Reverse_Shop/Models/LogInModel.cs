using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reverse_Shop.Models
{
    public class LogInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email/Login")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember me")]
        public bool Active { get; set; }
    }
}