using Skinet_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet_Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetAlProductsAsync();
        Task<IEnumerable<ProductBrand>> GetAllProductsBrandsAsync();
        Task<IEnumerable<ProductType>> GetAllProductTypesAsync();
    }
}
