namespace Easytrade.Api.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Easytrade.Api.IntegrationTests.Extensions;
    using Easytrade.Model.DbAccess;
    using System;

    public class ApiFactory<TStartup>
         : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly string _dbName;

        public ApiFactory(string dbName)
        {
            _dbName = dbName;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveDbContext<EasyTradeDbContext>();

                services.AddMvc(options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                });

                // Create a new service provider.
                var serviceProvider = new ServiceCollection { }
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                //Add a database context(ApplicationDbContext) using an in-memory
                // database for testing.
                services.AddDbContext<EasyTradeDbContext>((options, context) =>
                {
                    context.UseInMemoryDatabase(_dbName)
                        .UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                //Create a scope to obtain a reference to the database
                // context(ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var easyTradeDatabase = scopedServices.GetRequiredService<EasyTradeDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<ApiFactory<TStartup>>>();

                    // Ensure the database is created.
                    easyTradeDatabase.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        EasytradeDbSeeder.SeedData(easyTradeDatabase);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {Message}", ex.Message);
                        throw;
                    }
                }
            });
        }
    }
}
