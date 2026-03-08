using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvalutor<T> where T : BaseEntity
	{
		// Fun To Build Query 
		//_dbContext.Set<T>().where(p=>p.id ==id).Include(x => x.ProductBrand).Include(p=> p.ProductType)

		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> Spec) 
		{
			var Query = inputQuery; //_dbContext.Set<T>()
			if (Spec.Criteria is not null)
			{ 
			Query = Query.Where(Spec.Criteria); //_dbContext.Set<T>().where(p=>p.id ==id)
			}

			if (Spec.OrderBy is not null)
			{ 
				Query = Query.OrderBy(Spec.OrderBy);
			}
			if (Spec.OrderByDescending is not null)
			{ 
				Query = Query.OrderByDescending(Spec.OrderByDescending);
			}

			if(Spec.IsPaginationEnabled)
			{
				Query =Query.Skip(Spec.Skip).Take(Spec.Take);
			}
			
			Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
			//_dbContext.Set<T>().where(p=>p.id ==id).Include(x => x.ProductBrand).Include(p=> p.ProductType)

			return Query;
		}
	}
}
