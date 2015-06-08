using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Infrastructure.Classes;
using Infrastructure.Interface;

namespace Core.Classes.User
{
    public class UsersProduct
    {
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly HashWorker _hashWorker = new HashWorker();
        private readonly IProductRepository _productRepository = new ProductRepository();
       

        public List<Model.Product> UsersListofProducts(string loginHash)
        {
            var dbListOfProduct = _productRepository.Products().Where(p =>
            {
                var firstOrDefault = _userRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash);
                return firstOrDefault != null && p.IdSaler == firstOrDefault.Id;
            });
            var productList = new List<Model.Product>();
            foreach (var item in dbListOfProduct)
            {
                var productItem = new Model.Product()
                {
                    Category = item.Category,
                    Coast = item.Coast,
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    Info = item.Info,
                    Time = TimeValueProduct(item.Id)
                };
                productList.Add(productItem);
            }
            return productList;
        }

        public List<Model.Product> UsersSaleProductsList(string loginHash)
        {
            var firstOrDefault = _userRepository.Users().FirstOrDefault(u => u.LoginHash == loginHash);
            if (firstOrDefault == null)
            {
                return new List<Model.Product>();
            }
            int userId = firstOrDefault.Id;
            var productList = _productRepository.Products().Where(p => p.IdSaler == userId).ToList();
            return productList.Select(item => new Model.Product(item.Id, item.Name, item.Info, item.Image, item.Coast, TimeValueProduct(item.Id), item.Category, item.IdBuyer)).ToList();
        }

        private Time TimeValueProduct(int productId)
        {
            Time time = new Time();
            var product = _productRepository.SearchProduct(productId);
            DateTime fullDateTime = product.DateTime.AddHours(product.Time);
            var subTime = fullDateTime.Subtract(DateTime.Now);
            time.Hours = subTime.Hours;
            time.Minutes = subTime.Minutes;
            return time;
        }
    }
}
