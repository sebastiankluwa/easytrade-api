namespace Easytrade.Model.Repositories;
using Easytrade.Model.Domain.Bots;

public interface IOrderRepository<T> where T : Order
{
    Task<IEnumerable<T>> GetAllOpenOrdersAsync(long botId);
    Task<IEnumerable<T>> GetAllOrdersAsync();
    Task<IEnumerable<T>> GetAllOrdersByBotIdAsync(long botId);
    Task<T> GetOrderByIdAsync(long id);
    Task<T> CreateOrderAsync(T order);
    Task DeleteOrderByIdAsync(long id);
    Task<T> UpdateOrderAsync(T updatedOrder);
}