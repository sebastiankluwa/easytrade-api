namespace Easytrade.Logic.Services.Strategies.Orders.PlaceOrder
{
    using Binance.Net.Objects.Models.Spot;
    using BinanceApi.Client;
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Orders;
    using Easytrade.Model.Domain.Bots;
    using System.Threading.Tasks;

    public abstract class PlaceOrderStrategyBase : IPlaceOrderStrategy
    {
        protected PlaceOrderStrategyBase(IBinanceApiFacade binanceApiFacade)
        {
            BinanceApiFacade = binanceApiFacade;
        }

        protected IBinanceApiFacade BinanceApiFacade { get; }

        public abstract Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request);

        protected async Task<BinancePlacedOrder> PlaceBinanceOrder(PlaceOrderRequest request, decimal quantity)
        {
            var placedOrder = await BinanceApiFacade.PlaceOrder(
                request.Pair,
                request.Side.ToString(),
                quantity,
                request.Rate);

            return placedOrder;
        }

        protected Order MapOrder(long botId, BinancePlacedOrder binanceOrder, PlaceOrderRequest request)
        {
            var order = new Order
            {
                BotId = botId,
                ReferenceOrderId = binanceOrder.Id,
                Side = request.Side.ToString(),
                OrderDate = binanceOrder.CreateTime,
                Amount = binanceOrder.Quantity,
                Pair = request.Pair,
                Rate = binanceOrder.AverageFillPrice ?? binanceOrder.Price,
                Status = GetOrderStatus(binanceOrder),
                Fee = binanceOrder.Trades?.Select(p => p.Fee).Sum()
            };

            ApplyOrderMappings(order);

            return order;
        }

        protected virtual void ApplyOrderMappings(Order order)
        {

        }

        protected static OrderStatus GetOrderStatus(BinancePlacedOrder binanceOrder)
        {
            if (!Enum.TryParse<OrderStatus>(binanceOrder.Status.ToString(), true, out var orderStatus))
            {
                orderStatus = OrderStatus.Pending;
            }

            return orderStatus;
        }
    }
}
