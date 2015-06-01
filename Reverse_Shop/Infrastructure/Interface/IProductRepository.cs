using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;

namespace Infrastructure.Interface
{
    public interface IProductRepository
    {
        void Save(Product product);

        void Update(Product product);

        Product SearchProduct(int id);

        IEnumerable<Product> Products();

        void DeleteProduct(int id);

        void DeleteProduct(Product product);
    }
}
