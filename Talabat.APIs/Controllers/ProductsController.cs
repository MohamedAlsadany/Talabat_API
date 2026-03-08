using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entity;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat_APIs.DTOs;
using Talabat_APIs.Errors;
using Talabat_APIs.Helpers;

namespace Talabat_APIs.Controllers
{

	public class ProductsController : APIBaseController
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;


		public ProductsController(IMapper mapper 
			, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;

		}
		// Get All Products
		// BaseUrl/api/Products ->GET
	    [Authorize]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams Params)
		{
			var Spec = new ProductWithBrandAndTypeSpecifications(Params);
			var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(Spec);
		    var MappedProducts = _mapper.Map<IReadOnlyList<Product> ,IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFiltrationForCountAsync(Params);
			var Count =await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);
			return Ok(new Pagination<ProductToReturnDto>(Params.PageIndex, Params.PageSize, MappedProducts, Count));
		}

		// Get Products by Id
		// BaseUrl/api/Products/1 ->GET
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDto),200)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
		{
			var Spec = new ProductWithBrandAndTypeSpecifications(id);
			var products = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(Spec);
			if (products == null) { return BadRequest(new ApiResponse(404)); }
			var MappedProducts = _mapper.Map<Product, ProductToReturnDto>(products);
			return Ok(MappedProducts);
		}

		[HttpGet("Type")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetType() 
		{
			var Types =await _unitOfWork.Repository<ProductType>().GetAllAsync();
		return Ok(Types);
		}


		[HttpGet("Brand")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetBrand()
		{
			var Brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
			return Ok(Brands);
		}

	}
}
