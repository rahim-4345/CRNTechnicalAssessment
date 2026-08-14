using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection")));

            services.AddScoped<
                IProductRepository,
                ProductRepository>();

            services.AddScoped<
                IProductService,
                ProductService>();

            services.AddScoped<
                IAuthRepository,
                AuthRepository>();

            services.AddScoped<
                IAuthService,
                AuthService>();

            services.AddScoped<
                ITokenService,
                JwtTokenService>();

            return services;
        }
    }
}

//using Application.Interfaces;
//using Application.Services;
//using Infrastructure.Data;
//using Infrastructure.Repositories;
//using Infrastructure.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Infrastructure.Extensions
//{
//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddInfrastructure(
//            this IServiceCollection services,
//            IConfiguration configuration)
//        {
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(
//                    configuration.GetConnectionString(
//                        "DefaultConnection")));

//            services.AddScoped<IProductRepository, ProductRepository>();

//            services.AddScoped<IProductService, ProductService>();

//            services.AddScoped<IAuthRepository, AuthRepository>();

//            services.AddScoped<ITokenService, JwtTokenService>();

//            services.AddScoped<IProductService, ProductService>();

//            return services;
//        }
//    }
//}