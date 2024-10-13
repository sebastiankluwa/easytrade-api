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

        private BuyOrder? _relatedBuyOrder;
        private BinancePlacedOrder _binanceOrder = null!;

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
            _relatedBuyOrder = await _buyOrderRepository.GetOldestUnmatchedBuyOrderForBot(botId);
            
            var sellOrder = await CreateSellOrder(botId, request);

            var response = _mapper.Map<OrderDto>(sellOrder);

            return response;
        }
        private async Task<SellOrder> CreateSellOrder(long botId, PlaceOrderRequest request)
        {
            var sellQuantity = await GetOrderQuantity(botId);

            _binanceOrder = await PlaceBinanceOrder(request, sellQuantity);

            var sellOrder = _mapper.Map<SellOrder>(MapOrder(botId, _binanceOrder, request));

            await _sellOrderRepository.CreateOrderAsync(sellOrder);

            return sellOrder;
        }

        private async Task<decimal> GetOrderQuantity(long botId)
        {
            return _relatedBuyOrder?.Amount ?? (await _botRepository.GetBotByIdAsync(botId)).MinimumAllocation;
        }

        private ProfitLoss MapProfitLoss()
        {
            var profitLoss = new ProfitLoss
            {
                BuyOrderId = _relatedBuyOrder!.Id,
                CompletionDate = _binanceOrder.UpdateTime ?? DateTime.Now,
                Result = _relatedBuyOrder.Total -
                         _binanceOrder.Quantity * (_binanceOrder.AverageFillPrice ?? _binanceOrder.Price)
            };

            return profitLoss;
        }

        protected override void ApplyOrderMappings(Order order)
        {
            base.ApplyOrderMappings(order);
            order.ProfitLoss = _relatedBuyOrder?.Status == OrderStatus.Filled ? MapProfitLoss() : null;
        }
    }
}
