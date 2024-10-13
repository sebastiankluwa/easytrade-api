namespace Easytrade.UnitTests.Services.Strategies.Orders.PlaceOrder
{
    using AutoMapper;
    using BinanceApi.Client;
    using Easytrade.Logic.Services.Strategies.Orders.PlaceOrder;
    using Easytrade.Model.Repositories;
    using Easytrade.Model.Repositories.Impl;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class PlaceOrderStrategyFactoryTests
    {
        private readonly IServiceProvider _serviceProvider;

        public PlaceOrderStrategyFactoryTests()
        {
            var sc = new ServiceCollection();
            sc.AddScoped<IBotRepository>(sp => new Mock<IBotRepository>().Object);
            sc.AddScoped<IBinanceApiFacade>(sp => new Mock<IBinanceApiFacade>().Object);
            sc.AddScoped<IBuyOrderRepository>(sp => new Mock<IBuyOrderRepository>().Object);
            sc.AddScoped<ISellOrderRepository>(sp => new Mock<ISellOrderRepository>().Object);
            sc.AddScoped<IMapper>(sp => new Mock<IMapper>().Object);

            _serviceProvider = sc.BuildServiceProvider();
        }

        [Fact]
        public async Task CreatePlaceOrderStrategy_WhenBuyOrderSide_ShouldReturnValidStrategy()
        {
            // Arrange
            var factory = new PlaceOrderStrategyFactory(_serviceProvider);

            // Act
            var strategy = factory.Create(Contract.Dto.Orders.OrderSideDto.Buy);

            // Assert
            Assert.Equal(typeof(PlaceOrderBuyStrategy), strategy.GetType());
        }

        [Fact]
        public async Task CreatePlaceOrderStrategy_WhenSellOrderSide_ShouldReturnValidStrategy()
        {
            // Arrange
            var factory = new PlaceOrderStrategyFactory(_serviceProvider);

            // Act
            var strategy = factory.Create(Contract.Dto.Orders.OrderSideDto.Sell);

            // Assert
            Assert.Equal(typeof(PlaceOrderSellStrategy), strategy.GetType());
        }
    }
}
