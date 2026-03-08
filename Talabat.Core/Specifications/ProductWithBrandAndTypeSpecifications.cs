using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecifications :BaseSpecifications<Product>
	{
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams Params) 
            :base( P=> 
            (string.IsNullOrEmpty(Params.Search)|| P.Name.Contains(Params.Search))
            &&
            (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
                 &&
            (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
                 )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            if (!string.IsNullOrEmpty(Params.Sort))
            { 
            switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
					case "PriceDesc":
						AddOrderByDescending(P => P.Price);
						break;
                    default:
                        AddOrderBy(P=>P.Name);
                        break;



				}
            }

            //Products =100
            // PageSize =10
            //PageIndex =5

            // Skip => 40 =10*4 
            //Take => 10
            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageIndex);
		}
        // CTOR Is Used For Product By Id 
        public ProductWithBrandAndTypeSpecifications(int id) :base(P=>P.Id == id)
        {
			Includes.Add(P => P.ProductBrand);
			Includes.Add(P => P.ProductType);
		}
    }
}
