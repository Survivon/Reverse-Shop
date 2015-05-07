using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class Product
    {       
        [Key]
        
        public int Id { get; set; }

        public int IdSaler { get; set; }

        public int? IdBuyer { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public decimal? Coast { get; set; }

        public int Time { get; set; }

        public DateTime DateTime { get; set; }

        public string Category { get; set; }

        public bool? Active { get; set; }
    }
}
