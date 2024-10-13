namespace Easytrade.Logic.Services.Strategies.Orders.PlaceOrder
{
    using Easytrade.Contract.Dto.Orders;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class PlaceOrderStrategyFactory : IPlaceOrderStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PlaceOrderStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPlaceOrderStrategy Create(OrderSideDto orderSide)
        {
            return orderSide switch
            {
                OrderSideDto.Buy => ActivatorUtilities.CreateInstance<PlaceOrderBuyStrategy>(_serviceProvider),
                OrderSideDto.Sell => ActivatorUtilities.CreateInstance<PlaceOrderSellStrategy>(_serviceProvider),
                _ => throw new ArgumentException("Bad order side provided.")
            };
        }
    }
}
