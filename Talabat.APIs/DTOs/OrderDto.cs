using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entity.Order_Aggregate;

namespace Talabat_APIs.DTOs
{
	public class OrderDto
	{
		[Required]
		public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
