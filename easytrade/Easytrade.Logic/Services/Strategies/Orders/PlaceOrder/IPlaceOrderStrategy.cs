namespace Easytrade.Logic.Services.Strategies.Orders.PlaceOrder
{
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Orders;
    using System.Threading.Tasks;

    public interface IPlaceOrderStrategy
    {
        Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request);
    }
}
