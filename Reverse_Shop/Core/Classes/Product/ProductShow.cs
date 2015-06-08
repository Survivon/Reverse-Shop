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
    public class ProductShow
    {
        private readonly IProductRepository _iProductRepository = new ProductRepository();
        private readonly IUserRepository _iUserRepository = new UserRepository();


        #region ShowInfo

        public IEnumerable<Model.Product> ProductInPage(int pageNumber, int countProductInPage)
        {
            List<Model.Product> productList = new List<Model.Product>();
            IEnumerable<Infrastructure.Model.Product> allProducts = _iProductRepository.Products().Where(item => item.Active == true).ToList();
            var sortedList = from item in allProducts orderby item.DateTime descending, item select item;
            int maxCounterInItem = pageNumber * countProductInPage;
            int counterInItem = maxCounterInItem - countProductInPage;
            int count = 0;
            foreach (var item in sortedList)
            {
                if (counterInItem == maxCounterInItem)
                    break;
                if (count < counterInItem)
                {
                }
                else
                {
                    Model.Product newProduct = new Model.Product(item.Id, item.Name, item.Info, item.Image, item.Coast,
                        TimeValueProduct(item.Id),
                        item.Category, item.IdBuyer);
                    productList.Add(newProduct);
                    counterInItem++;
                }
                count++;
            }
            return productList.Count > 0 ? productList : null;
        }

        public int PageOfProductCount(int itemInPage)
        {
            return _iProductRepository.Products().Count() > itemInPage ? (int)Math.Ceiling((double)_iProductRepository.Products().Count() / itemInPage) : 1;
        }



        public Model.Product ProductInfo(int idProduct)
        {
            var productInDataBase = _iProductRepository.SearchProduct(idProduct);
            Model.Product thisProduct = new Model.Product();
            if (productInDataBase != null)
            {
                thisProduct = new Model.Product(
                    productInDataBase.Id,
                    productInDataBase.Name,
                    productInDataBase.Info,
                    productInDataBase.Image,
                    productInDataBase.Coast,
                    TimeValueProduct(productInDataBase.Id),
                    productInDataBase.Category,
                     productInDataBase.IdBuyer);
            }
            return thisProduct;
        }

        public Model.Product ProductInfo(string productName)
        {
            var productInDataBase = _iProductRepository.Products().FirstOrDefault(p => p.Name.Trim(' ') == productName);
            Model.Product thisProduct = new Model.Product();
            if (productInDataBase != null)
            {
                thisProduct = new Model.Product(
                    productInDataBase.Id,
                    productInDataBase.Name,
                    productInDataBase.Info,
                    productInDataBase.Image,
                    productInDataBase.Coast,
                    TimeValueProduct(productInDataBase.Id),
                    productInDataBase.Category,
                    productInDataBase.IdBuyer);
            }
            return thisProduct;
        }

        public IEnumerable<Model.Product> FilterProducts(string productCategory)
        {
            List<Model.Product> productList = new List<Model.Product>();
            IEnumerable<Infrastructure.Model.Product> allProducts = _iProductRepository.Products();
            foreach (var item in allProducts)
            {
                if (item.Category == productCategory)
                {
                    Model.Product newProduct = new Model.Product(item.Id, item.Name, item.Info, item.Image, item.Coast,
                        TimeValueProduct(item.Id),
                        item.Category, item.IdBuyer);
                    productList.Add(newProduct);
                }
            }
            return productList.Count > 0 ? productList : null;
        }

        public List<Model.Product> SearchProducts(string nameProduct)
        {
            IEnumerable<Infrastructure.Model.Product> allProducts = _iProductRepository.Products();
            List<Model.Product> productList = (from item in allProducts
                                         where item.Name.Contains(nameProduct)
                                         select new Model.Product(item.Id,
                                             item.Name, item.Info,
                                             item.Image, item.Coast,
                                             TimeValueProduct(item.Id),
                                             item.Category,
                                             item.IdBuyer))
                .ToList();
            return productList.Count > 0 ? productList : null;
        }

        public List<Model.Product> UsersProductsList(string loginHash)
        {
            var firstOrDefault = _iUserRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash);
            if (firstOrDefault == null)
            {
                return new List<Model.Product>();
            }
            int userId = firstOrDefault.Id;
            var productList = _iProductRepository.Products().Where(p => p.IdBuyer == userId).ToList();
            return productList.Select(item => new Model.Product(item.Id, item.Name, item.Info, item.Image, item.Coast, TimeValueProduct(item.Id), item.Category, item.IdBuyer)).ToList();

        }

        private Time TimeValueProduct(int productId)
        {
            Time time = new Time();
            var product = _iProductRepository.SearchProduct(productId);
            DateTime fullDateTime = product.DateTime.AddHours(product.Time);
            var subTime = fullDateTime.Subtract(DateTime.Now);
            time.Hours = subTime.Hours;
            time.Minutes = subTime.Minutes;
            return time;
        }

        #endregion
    }
}
