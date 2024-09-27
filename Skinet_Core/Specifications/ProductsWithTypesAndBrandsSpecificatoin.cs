using Skinet_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet_Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecificatoin : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecificatoin()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

    }
}
