using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Reverse_Shop.Models
{
    public class CoastModel
    {

        public int Id { get; set; }

        [Required]
        public decimal Coast { get; set; }
    }
}