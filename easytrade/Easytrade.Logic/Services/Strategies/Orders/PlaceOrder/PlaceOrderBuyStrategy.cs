namespace Easytrade.Logic.Services.Strategies.Orders.PlaceOrder
{
    using AutoMapper;
    using Binance.Net.Objects.Models.Spot;
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
            var bot = await _botRepository.GetBotByIdAsync(botId);
            var quantity = bot.MinimumAllocation;

            await Validate(bot);

            var binanceOrder = await PlaceBinanceOrder(request, quantity);

            var buyOrder = await CreateBuyOrder(botId, binanceOrder, request);

            var response = _mapper.Map<OrderDto>(buyOrder);

            return response;
        }

        private async Task<BuyOrder> CreateBuyOrder(long botId, BinancePlacedOrder binanceOrder, PlaceOrderRequest request)
        {
            var buyOrder = MapBuyOrder(botId, binanceOrder, request);

            await _buyOrderRepository.CreateOrderAsync(buyOrder);

            return buyOrder;
        }

        private static BuyOrder MapBuyOrder(long botId, BinancePlacedOrder binanceOrder, PlaceOrderRequest request)
        {
            var buyOrder = new BuyOrder
            {
                BotId = botId,
                ReferenceOrderId = binanceOrder.OriginalClientOrderId,
                Side = request.Side.ToString(),
                OrderDate = binanceOrder.CreateTime,
                Amount = binanceOrder.Quantity,
                Pair = request.Pair,
                Rate = binanceOrder.AverageFillPrice ?? binanceOrder.Price,
                Status = GetOrderStatus(binanceOrder),
                Fee = binanceOrder.Trades?.Select(p => p.Fee).Sum()
            };

            return buyOrder;
        }

        private async Task Validate(Bot bot)
        {
            var openBuyOrdersCount = await _buyOrderRepository.CountAllOpenBuyOrdersForBot(bot.Id);

            if (openBuyOrdersCount >= bot.MaxOpenPositions)
            {
                throw new InvalidOperationException($"Limit reached for the number of open orders: {bot.MaxOpenPositions}!");
            }
        }
    }
}
