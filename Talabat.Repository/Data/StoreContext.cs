using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
	{
        public StoreContext(DbContextOptions<StoreContext> option) : base(option)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            // FluentAPI
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Product> Product { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }


	}
}
