using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Services;
using Talabat_APIs.DTOs;
using Talabat_APIs.Errors;
using Talabat_APIs.Extensions;

namespace Talabat_APIs.Controllers
{
	public class AccountsController : APIBaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AccountsController(UserManager<AppUser> userManager
			  ,SignInManager<AppUser> signInManager
			  ,ITokenService tokenService 
			  ,IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			this._mapper = mapper;
		}
        // Register
        [HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model) 
		{
			if (CheckEmailExist(model.Email).Result.Value)
				return BadRequest(new ApiResponse(400, "Email Is Already In Use"));
			var user = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber,
			};
		     var Result =await _userManager.CreateAsync(user, model.Password);
			if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

			var ResultedUser = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token =/*"this is Token"*/ await _tokenService.GetTokenAsync(user, _userManager)
			};
			return Ok(ResultedUser);

		}

		// Login
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiResponse(401));
			var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
			return Ok(new UserDto 
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = /*"this is Token"*/ await _tokenService.GetTokenAsync(user, _userManager)
			});
		}
		[Authorize]
		[HttpGet("GetCurrentUser")]
		//BaseUrl/Api/Accounts/GetCurrentUser
		public async Task<ActionResult<UserDto>> GetCurrentUser() 
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(Email);
			var ReturnedObject = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.GetTokenAsync(user, _userManager)
			};
			return Ok(ReturnedObject);

		}
		[Authorize]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetCurrentUserAddress() 
		{
			var user = await _userManager.FindUserWithAdressAsync(User);
			var MappedAddress = _mapper.Map<Address,AddressDto>(user.Address);
			return Ok(MappedAddress);
		}
		[Authorize]
		[HttpPut("Address")]
		public async Task<ActionResult<AddressDto>> UpdateAddressAsync(AddressDto UpdateAddress) 
		{
			var user =await _userManager.FindUserWithAdressAsync(User);
			var MappedAddress = _mapper.Map<AddressDto, Address>(UpdateAddress);
			MappedAddress.Id  = user.Address.Id;
			user.Address=MappedAddress;
			var Result= await _userManager.UpdateAsync(user);
			if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(UpdateAddress);
		}
		[HttpGet("emailExists")]
		public async Task<ActionResult<bool>> CheckEmailExist(string Email) 
		{
		return await _userManager.FindByEmailAsync(Email) is not null;
		} 
	}
}
