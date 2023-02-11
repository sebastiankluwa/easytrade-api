namespace Easytrade.Logic.Services.Impl
{
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Orders;
    using Easytrade.Logic.Services;
    using Easytrade.Logic.Services.Strategies.Orders.PlaceOrder;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly IPlaceOrderStrategyFactory _placeOrderStrategyFactory;

        public OrderService(IPlaceOrderStrategyFactory placeOrderStrategyFactory)
        {
            _placeOrderStrategyFactory = placeOrderStrategyFactory;
        }

        public async Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request)
        {
            var strategy = _placeOrderStrategyFactory.Create(request.Side);

            var result = await strategy.PlaceOrder(botId, request);

            return result;
        }
    }
}
