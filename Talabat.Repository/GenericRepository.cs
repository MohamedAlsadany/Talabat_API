using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository( StoreContext dbContext)
        {
			_dbContext = dbContext;
		}

		#region Without Spec
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)

		 => await _dbContext.Set<T>().FindAsync(id); 
		#endregion

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
		{
			return await ApplySpecification(Spec).ToListAsync(); 
		}
		public async Task<T> GetEntityWithSpecAsync(ISpecifications<T> Spec)
		{
			return await ApplySpecification(Spec).FirstOrDefaultAsync();
		}

		public IQueryable<T> ApplySpecification(ISpecifications<T> Spec) 
		{
			return  SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec);
		}

		public async Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec)
		{
			return await ApplySpecification(Spec).CountAsync();
		}

		public async Task AddAsync(T item)
		=> await _dbContext.AddAsync(item);

		public void Delete(T item)
		=> _dbContext.Remove(item);

		public void Update(T item)
		=> _dbContext.Update(item);
	}
}
