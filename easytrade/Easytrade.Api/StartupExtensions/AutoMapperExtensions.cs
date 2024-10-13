namespace Easytrade.Api.StartupExtensions
{
    using Easytrade.Logic.Mappings;
    using Microsoft.Extensions.DependencyInjection;

    internal static class AutoMapperExtensions
    {
        /// <summary>
        /// Custom AutoMapper extension method for Easytrade.Api applying all defined profiles
        /// </summary>
        /// <param name="services"></param>
        internal static void AddAutoMapperWithProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EasytradeApiLogicProfile));
        }
    }
}
