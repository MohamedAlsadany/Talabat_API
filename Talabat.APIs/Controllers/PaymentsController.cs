using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entity;
using Talabat.Core.Services;
using Talabat_APIs.DTOs;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{

	public class PaymentsController :APIBaseController
	{
		private readonly IPaymentService _paymentService;
		private readonly IMapper _mapper;

		public PaymentsController(IPaymentService paymentService , IMapper mapper)
		{
			_paymentService = paymentService;
			_mapper = mapper;
		}
		// Create or Update EndPoint
		[ProducesResponseType(typeof(CustomerBasketDto) ,StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

		[HttpPost("{BasketId}")]
		public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntend(string BasketId) { 
		 
		 var CustomerBasket= await  _paymentService.CreateOrUpdatePaymentIntend(BasketId);
			if (CustomerBasket is null) return BadRequest(new ApiResponse(400, "There is a problem with your Basket"));
			var MappedBasket = _mapper.Map<CustomerBasket, CustomerBasketDto>(CustomerBasket);
			return Ok(MappedBasket);
		}
		

	}
}
