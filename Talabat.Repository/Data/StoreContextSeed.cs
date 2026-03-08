using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.Order_Aggregate;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (!dbContext.ProductBrands.Any())
			{
				var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
				if (Brands?.Count > 0)
				{
					foreach (var item in Brands)
					{
						await dbContext.Set<ProductBrand>().AddAsync(item);
					}
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.ProductType.Any())
			{
				var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

				if (Types?.Count > 0)
				{
					foreach (var item in Types)
					{
						await dbContext.Set<ProductType>().AddAsync(item);
					}
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.Product.Any()) {
				var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
				if (Products?.Count > 0)
				{
					foreach (var item in Products)
					{
						await dbContext.Set<Product>().AddAsync(item);
					}
					await dbContext.SaveChangesAsync();
				}
			}
			if (!dbContext.DeliveryMethods.Any())
			{
				var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
				var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
				if (DeliveryMethods?.Count > 0)
				{
					foreach (var item in DeliveryMethods)
					{
						await dbContext.Set<DeliveryMethod>().AddAsync(item);
					}
					await dbContext.SaveChangesAsync();
				}
			}

		}
	}  
}
