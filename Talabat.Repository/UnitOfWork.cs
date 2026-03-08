using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entity;
using Talabat.Core.Repository;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private Hashtable _Repositories;

		public UnitOfWork(StoreContext dbContext)
        {
			_dbContext = dbContext;
			_Repositories = new Hashtable();
		}
        public Task<int> CompleteAsync()
		=>	_dbContext.SaveChangesAsync();
		
		public ValueTask DisposeAsync()
		=>	_dbContext.DisposeAsync();

		public IGenericRepository<IEntity> Repository<IEntity>() where IEntity : BaseEntity
		{
			var Type =typeof(IEntity).Name; //Product ,Order
			if (!_Repositories.ContainsKey(Type))
			{ 	
				var Repository = new GenericRepository<IEntity>(_dbContext);
				_Repositories.Add(Type, Repository);
			}
			return (IGenericRepository<IEntity>) _Repositories[Type];
			
		}
	}
}
