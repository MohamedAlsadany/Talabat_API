using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Repository;

namespace Talabat.Core
{
	public interface IUnitOfWork :IAsyncDisposable
	{

		IGenericRepository<IEntity> Repository<IEntity>() where IEntity : BaseEntity;
		Task<int> CompleteAsync();

	}
}
