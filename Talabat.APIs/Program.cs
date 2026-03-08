
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Repository;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat_APIs.Errors;
using Talabat_APIs.Extensions;
using Talabat_APIs.Helpers;
using Talabat_APIs.MiddleWares;

namespace Talabat_APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);



			#region Configure Services Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreContext>(Option =>
			{
				Option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddDbContext<AppIdentityDbContext>(Option => 
			Option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
			);
			builder.Services.AddSingleton<IConnectionMultiplexer>(Options => 
			{
				var Connection = builder.Configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(Connection);
			} );

			builder.Services.AddApplicationServices();
			builder.Services.AddIdentityServices(builder.Configuration);

			builder.Services.AddCors(Option =>
			{
				Option.AddPolicy("MyPolicy", options =>
				{
					options.AllowAnyHeader();
					options.AllowAnyMethod();
					options.WithOrigins(builder.Configuration["FrontBaseUrl"]);
				});

			});

			#endregion

			var app = builder.Build();
			using var Scoped = app.Services.CreateScope();// Group of services lifetime scooped

			var services = Scoped.ServiceProvider; // services its self
			var LoggerFactor = services.GetRequiredService<ILoggerFactory>();


			try
			{
				var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
				await IdentityDbContext.Database.MigrateAsync(); // Update-Database	
				var UserManager = services.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextSeed.SeedUserAsync(UserManager);

				var dbContext = services.GetRequiredService<StoreContext>();// Ask CLR for creating object from DbContext Explicitly
				await dbContext.Database.MigrateAsync(); // Update-Database	

				
				await StoreContextSeed.SeedAsync(dbContext);

			}
			catch (Exception ex)
			{
				var Logger = LoggerFactor.CreateLogger<Program>();
				Logger.LogError(ex, "An Error Occured During Appling The Migration");
			}

			#region Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMiddleWare>();
				
				app.UseSwaggerMiddlewares();

			}
			app.UseStaticFiles();
			app.UseStatusCodePagesWithReExecute("/Error/{0}");
			app.UseHttpsRedirection();
			app.UseCors("MyPolicy");
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
