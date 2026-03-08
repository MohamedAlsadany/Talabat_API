using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;

namespace Talabat.Core.Services
{
	public interface IPaymentService
	{
		// Fun to create or update Payment Intend
		Task<CustomerBasket> CreateOrUpdatePaymentIntend(string BasketId);
	}
}
