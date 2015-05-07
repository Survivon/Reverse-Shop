using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Interface;
using Core.Model;

namespace Core
{
    public class ProductWorker
    {
        private readonly IProductRepository _irepository = new ProductRepository();

        #region ShowInfo

        public IEnumerable<Product> ProductInPage(int pageNumber, int countProductInPage)
        {
            List<Product> productList = new List<Product>();
            IEnumerable<Infrastructure.Model.Product> allProducts = _irepository.Products().Where(item => item.Active == true).ToList();
            var sortedList = from item in allProducts orderby item.DateTime descending, item select item;
            int maxCounterInItem = pageNumber*countProductInPage;
            int counterInItem = maxCounterInItem - countProductInPage;
            foreach (var item in sortedList)
            {
                if (counterInItem == maxCounterInItem)
                    break;
                Product newProduct = new Product(item.Id, item.Name, item.Info, item.Image, item.Coast,
                    TimeValueProduct(item.Id),
                    item.Category);
                productList.Add(newProduct);
                counterInItem++;
            }
            return productList.Count > 0 ? productList : null;
        }

        private Time TimeValueProduct(int productId)
        {
            Time time = new Time();
            var product = _irepository.SearchProduct(productId);
            DateTime fullDateTime = product.DateTime.AddHours(product.Time);
            var subTime = fullDateTime.Subtract(DateTime.Now);
            time.Hours = subTime.Hours;
            time.Minutes = subTime.Minutes;
            return time;
        }

        public Product ProductInfo(int idProduct)
        {
            var productInDataBase = _irepository.SearchProduct(idProduct);
            Product thisProduct = new Product();
            if (productInDataBase != null)
            {
                thisProduct = new Product(
                    productInDataBase.Id,
                    productInDataBase.Name,
                    productInDataBase.Info,
                    productInDataBase.Image,
                    productInDataBase.Coast,
                    TimeValueProduct(productInDataBase.Id),
                    productInDataBase.Category);
            }
            return thisProduct;
        }

        public IEnumerable<Product> FilterProducts(string productCategory)
        {
            List<Product> productList = new List<Product>();
            IEnumerable<Infrastructure.Model.Product> allProducts = _irepository.Products();
            foreach (var item in allProducts)
            {
                if (item.Category == productCategory)
                {
                    Product newProduct = new Product(item.Id, item.Name, item.Info, item.Image, item.Coast,
                        TimeValueProduct(item.Id),
                        item.Category);
                    productList.Add(newProduct);
                }
            }
            return productList.Count > 0 ? productList : null;
        }

        public List<Product> SearchProducts(string nameProduct)
        {
            IEnumerable<Infrastructure.Model.Product> allProducts = _irepository.Products();
            List<Product> productList = (from item in allProducts
                where item.Name.Contains(nameProduct)
                select new Product(item.Id,
                    item.Name, item.Info,
                    item.Image, item.Coast,
                    TimeValueProduct(item.Id),
                    item.Category))
                .ToList();
            return productList.Count > 0 ? productList : null;
        }

        #endregion

        #region SaveOrUpdate

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
                Image = product.Image,
                Time = product.Time
            };
            try
            {
                _irepository.SaveOrUpdate(newProduct);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProduct(SaveProduct product)
        {
            var changeProduct =  _irepository.SearchProduct(product.Id);
            if (changeProduct.Image != product.Image)
                changeProduct.Image = product.Image;
            if (changeProduct.Info != product.Info)
                changeProduct.Info = product.Info;
            if (changeProduct.Category != product.Category)
                changeProduct.Category = product.Category;
            try
            {
                _irepository.SaveOrUpdate(changeProduct);
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
                _irepository.SaveOrUpdate(activeteProduct);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool ProductCoast(int productId,int coastValue)
        {
            decimal productCoast = _irepository.SearchProduct(productId).Coast ?? decimal.MaxValue;
            return productCoast>coastValue;
        }

        public bool AddNewBuyer(int productId, int buyerId, decimal productCoast)
        {
            var product = _irepository.SearchProduct(productId);
            product.IdBuyer = buyerId;
            product.Coast = productCoast;
            try
            {
                _irepository.SaveOrUpdate(product);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    #endregion

        #region Delete

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

        #endregion
    }
}
