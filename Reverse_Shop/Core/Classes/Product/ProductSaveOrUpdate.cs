using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Infrastructure.Classes;
using Infrastructure.Interface;

namespace Core.Classes.Product
{
    public class ProductSaveOrUpdate
    {
        private readonly IProductRepository _irepository = new ProductRepository();

        public bool SaveProduct(SaveProduct product)
        {
            var newProduct = new Infrastructure.Model.Product
            {
                IdSaler = product.IdSaler,
                Name = product.Name,
                Info = product.Info,
                DateTime = DateTime.Now,
                Coast = product.Coast,
                Category = product.Category,
                Time = product.Time,
                Active = true
            };
            try
            {
                _irepository.Save(newProduct);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProduct(Model.Product product)
        {
            var changeProduct = _irepository.SearchProduct(product.Id);
            if (product.Image != null)
            {
                if (changeProduct.Image != product.Image)
                    changeProduct.Image = product.Image;
            }
            if (product.Info != null)
            {
                if (changeProduct.Info != product.Info)
                    changeProduct.Info = product.Info;
            }
            if (product.Info != null)
            {
                if (changeProduct.Category != product.Category)
                    changeProduct.Category = product.Category;
            }
            try
            {
                _irepository.Update(changeProduct);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool ActivateProduct(int productId, bool mode)
        {
            try
            {
                var activeteProduct = _irepository.SearchProduct(productId);
                activeteProduct.Active = mode;
                _irepository.Update(activeteProduct);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool ProductCoast(int productId, decimal coastValue)
        {
            decimal productCoast = _irepository.SearchProduct(productId).Coast ?? decimal.MaxValue;
            return productCoast > coastValue;
        }

        public bool AddNewBuyer(int productId, int buyerId, decimal productCoast)
        {
            var product = _irepository.SearchProduct(productId);
            product.IdBuyer = buyerId;
            product.Coast = productCoast;
            try
            {
                _irepository.Update(product);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                _irepository.DeleteProduct(productId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
