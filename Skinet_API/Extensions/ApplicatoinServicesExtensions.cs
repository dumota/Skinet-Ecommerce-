using Microsoft.AspNetCore.Mvc;
using Skinet_API.Errors;
using Skinet_Core.Interfaces;
using Skinet_Infrastructure.Data;

namespace Skinet_API.Extensions
{
    public static class ApplicatoinServicesExtensions  
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorReposnse = new ApiValidationErroResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorReposnse);
                };
            });

            return services;
        }
    }
}
