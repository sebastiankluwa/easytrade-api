using Binance.Net;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BinanceApi.Client.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBinanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var binanceApiSettings = new BinanceApiSettings();
            configuration.GetSection(nameof(BinanceApiSettings)).Bind(binanceApiSettings);

            services.AddBinance((restClientOptions, socketClientOptions) => {
                restClientOptions.ApiCredentials = new ApiCredentials(binanceApiSettings.ApiKey, binanceApiSettings.SecretKey);
                restClientOptions.LogLevel = LogLevel.Trace;

                socketClientOptions.ApiCredentials = new ApiCredentials(binanceApiSettings.ApiKey, binanceApiSettings.SecretKey);
            });

            return services;
        }
    }
}
