using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reverse_Shop.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public int IdSaler { get; set; }

        public bool IsChoose { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public decimal Coast { get; set; }

        public double Time { get; set; }

        public string Category { get; set; }
    }
}