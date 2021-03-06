﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Infrastructure.Interface;
using Infrastructure.Model;

namespace Infrastructure.Classes
{
    public class ProductRepository:IProductRepository
    {
        private readonly ReverseShopContext _dbContext = new ReverseShopContext();
        public void Save(Product product) 
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
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
