using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Interface;
using Core.Model;

namespace Core
{
    public class UserWorker
    {
        private IProductRepository Irepository = new ProductRepository();

        public Product AllProducts()
        {
            var products = Irepository.SearchProduct(1);
            Product product = new Product();
            product.Id = products.Id;
            DateTime dateTime = products.DateTime.AddHours(products.Time);
            TimeSpan timeSpan = dateTime.Subtract(DateTime.Now);
            Time t = new Time();
            t.Hours = timeSpan.Hours;
            t.Minutes=timeSpan.Minutes;
            product.Time = t;
            return product;
        }
    }
}
