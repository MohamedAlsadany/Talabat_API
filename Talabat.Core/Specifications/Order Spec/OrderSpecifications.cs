using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_Spec
{
	public class OrderSpecifications :BaseSpecifications<Order>
	{
		public OrderSpecifications(string Email) : base(O => O.BuyerEmail == Email) 
		{
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);
			AddOrderByDescending(O => O.OrderDate);
		}
        public OrderSpecifications(string Email ,int orderId):base(O =>O.BuyerEmail ==Email && O.Id ==orderId)
        {
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);
		}
    }
}
