using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Classes.Product;
using Core.Model;

namespace Reverse_Shop.Controllers
{
    public class ApiProductController : ApiController
    {
        private static readonly ProductShow ProductShow = new ProductShow();
        private static readonly ProductSaveOrUpdate ProductSaveOrUpdate = new ProductSaveOrUpdate();

        public Product Get(int id)
        {
            return ProductShow.ProductInfo(id);
        }

        public IEnumerable<Product> Get()
        {
            return ProductShow.ProductInPage(1, 20);
        }

        public void Edit( Product product)
        {
            var productSavty = ProductShow.ProductInfo(product.Id);
            if (product.BuyerId != productSavty.BuyerId)
            {
                ProductSaveOrUpdate.AddNewBuyer(product.Id, product.BuyerId != null ? 1 : 0, product.Coast != null ? 1 : 0);
            }
            if (product.Info != productSavty.Info || product.Category != productSavty.Category)
            {
                ProductSaveOrUpdate.UpdateProduct(product);
            }
        }

        public void Put(SaveProduct product)
        {
            ProductSaveOrUpdate.SaveProduct(product);
        }

        public void Delete(int id)
        {
            ProductSaveOrUpdate.DeleteProduct(id);
        }
    }
}
