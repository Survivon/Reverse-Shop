using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;
using Infrastructure.Interface;

namespace Infrastructure
{
    public class ProductRepository:IProductRepository
    {
        private ReverseShopContext _dbContext = new ReverseShopContext();
        public void SaveOrUpdate(Product product) 
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public Product SearchProduct(int id) 
        {
            return _dbContext.Products.Find(id);
        }

        public IEnumerable<Product> Products() 
        {
            return _dbContext.Products.ToList();
        }

        public void DeleteProduct(int id) 
        {
            _dbContext.Products.Remove(_dbContext.Products.Find(id));
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(Product product) 
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
