using Binance.Net.Clients;
using Kucoin.Net.Clients;
using Microsoft.OpenApi.Models;
using PeanutTestProject.Core.Services;
using PeanutTestProject.Core.Services.ExternalClients;
using PeanutTestProject.Core.Services.Interfaces;

namespace PeanutTestProject.Core.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddSingleton<KucoinRestClient>();
        services.AddSingleton<BinanceRestClient>();

        services.AddTransient<IExternalClient, BinanceClient>();
        services.AddTransient<IExternalClient, KuCoinClient>();

        services.AddScoped<IExchangeService, ExchangeService>();

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Peanut Trade Test Project",
                Version = "v1",
                Description = "Peanut Trade Test Project"
            });
        });

        return services;
    }
}