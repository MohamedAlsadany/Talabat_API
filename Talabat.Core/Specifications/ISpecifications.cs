using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Specifications
{
	public interface ISpecifications <T> where T : BaseEntity
	{
		//_dbContext.Product.where(p=>p.id ==id).Include(x => x.ProductBrand).Include(p=> p.ProductType)

		// Sign for Property for Where Condition [where(p=>p.id ==id)]
		public Expression<Func<T ,bool>> Criteria { get; set; }

		// Sign for Property for List of include [Include(x => x.ProductBrand).Include(p=> p.ProductType)]
		public List<Expression<Func<T ,object>>> Includes { get; set; }

		// prop for OrderBy [OrderBy(P=>P.Name)]
        public Expression<Func<T,object>> OrderBy { get; set; }

		// prop for OrderByDescending [OrderBy(P=>P.Name)]
		public Expression<Func<T, object>> OrderByDescending { get; set; }

        // Take()
        public int Take { get; set; }

        //Skip()
        public int Skip { get; set; }

        public bool IsPaginationEnabled { get; set; }
    }
}
