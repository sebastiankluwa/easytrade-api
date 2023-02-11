namespace Easytrade.Logic.Services;
using Easytrade.Contract.Dto.Orders;
using Easytrade.Contract.Requests.Orders;

public interface IOrderService
{
    Task<OrderDto> PlaceOrder(long botId, PlaceOrderRequest request);
}