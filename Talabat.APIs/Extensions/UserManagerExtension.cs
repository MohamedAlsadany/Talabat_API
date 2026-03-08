using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entity.Identity;

namespace Talabat_APIs.Extensions
{
	public static class UserManagerExtension
	{
		public static async Task<AppUser?> FindUserWithAdressAsync(this UserManager<AppUser> userManager ,ClaimsPrincipal User)
		{
		 var Email = User.FindFirstValue(ClaimTypes.Email);
			var user =await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email ==Email);
			return user;
		}
	}
}
