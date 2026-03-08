using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository
{
	public interface IGenericRepository<T> where T:BaseEntity
	{
		// Get All 
		#region Without Specifications
		Task<IReadOnlyList<T>> GetAllAsync();
		// Get All by id
		Task<T> GetByIdAsync(int id);
		#endregion
		#region  With Specifications
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
		Task<T> GetEntityWithSpecAsync(ISpecifications<T> Spec);
		Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec);
		Task AddAsync(T item);
		void Delete (T item);
		void Update(T item);


		#endregion

		
	}
}
