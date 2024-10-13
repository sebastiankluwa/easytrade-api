namespace Easytrade.Logic.Services.Impl
{
    using BinanceApi.Client;
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Orders;
    using Easytrade.Logic.Repositories;
    using Easytrade.Logic.Services;
    using Easytrade.Logic.Services.Strategies.Orders.PlaceOrder;
    using Easytrade.Model.Repositories;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly IPlaceOrderStrategyFactory _placeOrderStrategyFactory;
        private readonly IBinanceApiFacade _binanceApiFacade;
        private readonly IBotRepository _botRepository;
        private readonly ICompositeOrdersRepository _compositeOrdersRepository;

        public OrderService(IPlaceOrderStrategyFactory placeOrderStrategyFactory,
            IBinanceApiFacade binanceApiFacade,
            IBotRepository botRepository,
            ICompositeOrdersRepository compositeOrdersRepository)
        {
            _placeOrderStrategyFactory = placeOrderStrategyFactory;
            _binanceApiFacade = binanceApiFacade;
            _botRepository = botRepository;
            _compositeOrdersRepository = compositeOrdersRepository;
        }

        public async Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request)
        {
            var strategy = _placeOrderStrategyFactory.Create(request.Side);

            var result = await strategy.PlaceOrder(botId, request);

            return result;
        }

        public async Task CancelOrder(long botId, long orderId)
        {
            await _binanceApiFacade.CancelOrder(orderId);
        }
    }
}
