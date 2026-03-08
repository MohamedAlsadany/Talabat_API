using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entity;
using Talabat.Core.Repository;
using Talabat_APIs.DTOs;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{
	public class BasketController : APIBaseController
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository ,IMapper mapper)
        {
		   _basketRepository = basketRepository;
			this._mapper = mapper;
		}
        //Get Or ReCreate Basket
        [HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId) 
		{
			var Basket =await _basketRepository.GetBasketAsync(BasketId);
			return Basket is null ? new CustomerBasket(BasketId) : Basket;
		}

		// Update Or Create 
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket) 
		{
			var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
		 var UpdatedOrCreatedBasket =await _basketRepository.UpdateBasketAsync(MappedBasket);
			if (UpdatedOrCreatedBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(UpdatedOrCreatedBasket);
		}

		// Delete
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string BasketId) 
		{
		return await _basketRepository.DeleteBasketAsync(BasketId);
		}
	}
}
