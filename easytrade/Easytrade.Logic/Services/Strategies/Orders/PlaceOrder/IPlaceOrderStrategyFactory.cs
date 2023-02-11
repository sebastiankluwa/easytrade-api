namespace Easytrade.Logic.Services.Strategies.Orders.PlaceOrder
{
    using Easytrade.Contract.Dto.Orders;

    public interface IPlaceOrderStrategyFactory
    {
        IPlaceOrderStrategy Create(OrderSideDto orderSide);
    }
}
