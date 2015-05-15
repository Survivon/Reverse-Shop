﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reverse_Shop.Models
{
    public class ProductModel
    {
        public string Name { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public decimal? Coast { get; set; }

        public int Time { get; set; }

        public string Category { get; set; }
        
    }
}