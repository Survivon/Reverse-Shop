using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reverse_Shop.Models
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        public string Image { get; set; }

        
        public decimal? Coast { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public string Category { get; set; }
        
    }
}