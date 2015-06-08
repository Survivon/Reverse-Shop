using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
   public class SaveProduct
    {

        public int IdSaler { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public decimal? Coast { get; set; }

        public int Time { get; set; }

        public string Category { get; set; }

        public bool? Active { get; set; }
    }
}
