using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entity.Order_Aggregate;
using Talabat.Core.Services;
using Talabat_APIs.DTOs;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{

	public class OrdersController : APIBaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService ,IMapper mapper)
        {
			_orderService = orderService;
			_mapper = mapper;
		}
		// Create Order
		[ProducesResponseType(typeof(Order) ,StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status400BadRequest)]

		[HttpPost] // Post => BaseUrl/Api/Orders
		[Authorize]
		public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto) 
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var MappedAddress =_mapper.Map<AddressDto ,Address>(orderDto.ShippingAddress);
			var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);
			if (Order is null) return BadRequest(new ApiResponse(400, "There is a Problem With Your Order"));
			return Ok(Order);
		
		}
		[ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser() 
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var Orders =await _orderService.GetOrdersForSpecificUserAsync(BuyerEmail);
			if (Orders is null) return NotFound(new ApiResponse(404, "There is no orders For This User"));
			var MappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
			return Ok(MappedOrders);
		
		}
		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id) 
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var order =await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);
			if (order is null) return NotFound(new ApiResponse(404, $"There is no order with id ={id} For this user"));
			var MappedOrders = _mapper.Map<Order,OrderToReturnDto>(order);
			return Ok(MappedOrders);
		}
		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods() 
		{
			var DeliveryMethods =await _orderService.GetDeliveryMethodAsync();
			return Ok(DeliveryMethods);
		}

	}
}
