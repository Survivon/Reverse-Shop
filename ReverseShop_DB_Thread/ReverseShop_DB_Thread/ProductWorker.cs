using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Interface;

namespace ReverseShop_DB_Thread
{
    public class ProductWorker
    {
        private readonly IProductRepository _irepository = new ProductRepository();
        public Time TimeValueProduct(int productId)
        {
            Time time = new Time();
            var product = _irepository.SearchProduct(productId);
            DateTime fullDateTime = product.DateTime.AddHours(product.Time);
            var subTime = fullDateTime.Subtract(DateTime.Now);
            time.Hours = subTime.Hours;
            time.Minutes = subTime.Minutes;
            return time;
        }
    }
}
