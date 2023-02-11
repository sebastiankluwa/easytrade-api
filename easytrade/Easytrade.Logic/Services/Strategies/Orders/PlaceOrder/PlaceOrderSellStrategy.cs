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

    public class PlaceOrderSellStrategy : PlaceOrderStrategyBase
    {
        private readonly ISellOrderRepository _sellOrderRepository;
        private readonly IBuyOrderRepository _buyOrderRepository;
        private readonly IBotRepository _botRepository;
        private readonly IMapper _mapper;

        public PlaceOrderSellStrategy(IBinanceApiFacade binanceApiFacade,
            ISellOrderRepository sellOrderRepository,
            IBuyOrderRepository buyOrderRepository,
            IBotRepository botRepository,
            IMapper mapper) : base(binanceApiFacade)
        {
            _sellOrderRepository = sellOrderRepository;
            _buyOrderRepository = buyOrderRepository;
            _botRepository = botRepository;
            _mapper = mapper;
        }

        public override async Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request)
        {
            var relatedBuyOrder = await _buyOrderRepository.GetOldestUnmatchedBuyOrderForBot(botId);

            var sellQuantity = await GetOrderQuantity(botId, relatedBuyOrder);
            var binanceOrder = await PlaceBinanceOrder(request, sellQuantity);

            var sellOrder = await CreateSellOrder(botId, binanceOrder, request, relatedBuyOrder);

            var response = _mapper.Map<OrderDto>(sellOrder);

            return response;
        }

        private async Task<decimal> GetOrderQuantity(long botId, BuyOrder? relatedBuyOrder)
        {
            if (relatedBuyOrder != null)
            {
                return relatedBuyOrder.Amount;
            }

            var bot = await _botRepository.GetBotByIdAsync(botId);

            return bot.MinimumAllocation;
        }

        private async Task<SellOrder> CreateSellOrder(long botId, BinancePlacedOrder binanceOrder, PlaceOrderRequest request, BuyOrder relatedBuyOrder)
        {
            var sellOrder = MapSellOrder(botId, binanceOrder, request, relatedBuyOrder);

            await _sellOrderRepository.CreateOrderAsync(sellOrder);

            return sellOrder;
        }

        private static SellOrder MapSellOrder(long botId, BinancePlacedOrder placedOrder, PlaceOrderRequest request, BuyOrder relatedBuyOrder)
        {
            var sellOrder = new SellOrder
            {
                BotId = botId,
                ReferenceOrderId = placedOrder.OriginalClientOrderId,
                Side = request.Side.ToString(),
                OrderDate = placedOrder.CreateTime,
                Amount = placedOrder.Quantity,
                Pair = request.Pair,
                Rate = placedOrder.AverageFillPrice ?? placedOrder.Price,
                Status = GetOrderStatus(placedOrder),
                Fee = placedOrder.Trades?.Select(p => p.Fee).Sum(),
                ProfitLoss = relatedBuyOrder.Status == OrderStatus.Filled
                    ? MapProfitLoss(placedOrder, relatedBuyOrder)
                    : null
            };

            return sellOrder;
        }

        private static ProfitLoss MapProfitLoss(BinancePlacedOrder placedOrder, BuyOrder relatedBuyOrder)
        {
            var profitLoss = new ProfitLoss
            {
                BuyOrderId = relatedBuyOrder.Id,
                CompletionDate = placedOrder.UpdateTime ?? DateTime.Now,
                Result = relatedBuyOrder.Total -
                         placedOrder.Quantity * (placedOrder.AverageFillPrice ?? placedOrder.Price)
            };

            return profitLoss;
        }
    }
}
