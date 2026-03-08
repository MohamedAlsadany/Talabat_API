using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;

namespace Talabat.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager) 
		{
			if (!userManager.Users.Any()) 
			{
				var User = new AppUser()
				{
					DisplayName = "Mohamed Maher",
					Email = "MohamedMaher12@gmail.com",
					UserName = "mohamedmaher.route",
					PhoneNumber = "1234567890",
				};
			await userManager.CreateAsync(User,"Pa$$w0rd");
			}

		}
	}
}
