namespace Easytrade.Logic.Services.Strategies.Orders.PlaceOrder
{
    using AutoMapper;
    using BinanceApi.Client;
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Orders;
    using Easytrade.Model.Domain.Bots;
    using Easytrade.Model.Repositories;
    using Easytrade.Model.Repositories.Impl;
    using System;
    using System.Threading.Tasks;

    public class PlaceOrderBuyStrategy : PlaceOrderStrategyBase
    {
        private readonly IBotRepository _botRepository;
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly IMapper _mapper;

        private Bot _bot = null!;

        public PlaceOrderBuyStrategy(IBotRepository botRepository,
            IBinanceApiFacade binanceApiFacade,
            IBuyOrderRepository buyOrderRepository,
            IMapper mapper) : base(binanceApiFacade)
        {
            _botRepository = botRepository;
            _buyOrderRepository = buyOrderRepository;
            _mapper = mapper;
        }

        public override async Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request)
        {
            _bot = await _botRepository.GetBotByIdAsync(botId);

            await Validate();

            var buyOrder = await CreateBuyOrder(botId, request);

            var response = _mapper.Map<OrderDto>(buyOrder);

            return response;
        }

        private async Task<BuyOrder> CreateBuyOrder(long botId, PlaceOrderRequest request)
        {
            var buyQuantity = GetOrderQuantity();

            var binanceOrder = await PlaceBinanceOrder(request, buyQuantity);

            BuyOrder buyOrder = _mapper.Map<BuyOrder>(MapOrder(botId, binanceOrder, request));

            await _buyOrderRepository.CreateOrderAsync(buyOrder);

            return buyOrder;
        }

        private async Task Validate()
        {
            var openBuyOrdersCount = await _buyOrderRepository.CountAllOpenBuyOrdersForBot(_bot.Id);

            if (openBuyOrdersCount >= _bot.MaxOpenPositions)
            {
                throw new InvalidOperationException($"Limit reached for the number of open orders: {_bot.MaxOpenPositions}!");
            }
        }

        private decimal GetOrderQuantity() => _bot.MinimumAllocation;
    }
}
