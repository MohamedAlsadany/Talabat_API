using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entity;

namespace Talabat_APIs.DTOs
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }
		public List<BasketItemDto> Items { get; set; }
		public string? PaymentIntendId { get; set; }
		public string? ClientSecret { get; set; }
		public int? DeliveryMethodId { get; set; }

	}
}
