using Skinet_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skinet_Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecificatoin : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecificatoin(string sort, int? brandId, int? typeId ) 
            :base(x =>
            (!brandId.HasValue || x.ProductBrandId == brandId) &&
            (!typeId.HasValue || x.ProductTypeId == typeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc": AddOrderBy(p => p.Price); break;
                    case "priceDesc": AddOrderByDesc(p => p.Price); break;
                    default: AddOrderBy(p => p.Name); break;

                }
            }
        }

        public ProductsWithTypesAndBrandsSpecificatoin(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
