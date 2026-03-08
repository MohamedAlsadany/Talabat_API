using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Services;
using Talabat_APIs.Errors;
using Talabat_APIs.Helpers;

namespace Talabat_APIs.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services ) 
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
			//services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfiles));
			services.Configure<ApiBehaviorOptions>(Options => {

				Options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					// ModelState :Dic [KeyValuePair]
					// Key : Name of Parameter
					// Value : Errors

					var Errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														.SelectMany(P => P.Value.Errors)
														.Select(E => E.ErrorMessage)
														.ToArray();
					var ValidationErrorResponse = new ApiValidationErrorResponse()
					{
						Errors = Errors
					};
					return new BadRequestObjectResult(ValidationErrorResponse);
				};
			});
			return services;
		}
	}
}
