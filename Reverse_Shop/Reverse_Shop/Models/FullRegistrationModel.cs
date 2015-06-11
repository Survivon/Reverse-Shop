using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reverse_Shop.Models
{
    public class FullRegistrationModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}