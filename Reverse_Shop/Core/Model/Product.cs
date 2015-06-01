using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Product
    {
        public Product(int id, string name,string info,string image,decimal? coast,Time time,string category,int? buyerId)
        {
            this.Id = id;
            
            this.Name = name;
            this.Info = info;
            this.Image = image;
            this.Coast = coast;
            this.Time = time;
            this.Category = category;
            this.BuyerId = buyerId;
        }
        public Product()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? BuyerId { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public decimal? Coast { get; set; }

        public Time Time { get; set; }

        public string Category { get; set; }
    }

    public class Time
    {
        public int Hours { get; set; }

        public int Minutes { get; set; }
    }
}
