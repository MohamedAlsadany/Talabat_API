using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entity.Order_Aggregate
{
	public class OrderItem :BaseEntity
	{
		public OrderItem()
		{
		}

		public OrderItem(ProductItemOrdered product, int quantity, decimal price)
		{
			this.product = product;
			Quantity = quantity;
			this.price = price;
		}

		public ProductItemOrdered product { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
    }
}
