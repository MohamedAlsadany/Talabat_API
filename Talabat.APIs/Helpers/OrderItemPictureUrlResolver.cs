using AutoMapper;
using Talabat.Core.Entity.Order_Aggregate;
using Talabat_APIs.DTOs;

namespace Talabat_APIs.Helpers
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.product.PictureUrl))
				return $"{_configuration["ApiBaseUrl"]}{source.product.PictureUrl}";
			return string.Empty;
		}
	}
}
