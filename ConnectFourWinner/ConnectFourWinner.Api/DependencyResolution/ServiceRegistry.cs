using ConnectFourWinner.Services;
using ConnectFourWinner.Services.Interfaces;
using ConnectFourWinner.Services.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ConnectFourWinner.Api.DependencyResolution
{
    public static class ServiceRegistry
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {           
            services.AddScoped<IValidator, BoardValidator>();
            services.AddScoped<IValidator, LengthValidator>();
            services.AddScoped<IValidator, SymbolPiecesValidator>();
            services.AddScoped<IValidator, TeamNumberPiecesValidator>();
            services.AddScoped<IPlayValidator, PossiblePlayValidator>();            

            services.AddScoped<IServiceConnectFour, ServiceConnectFour>(_ => new ServiceConnectFour(_.GetServices<IValidator>().ToList(), _.GetService<IPlayValidator>()));   


            return services;
        }
    }
}
