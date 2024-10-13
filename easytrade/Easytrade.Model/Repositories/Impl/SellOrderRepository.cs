namespace Easytrade.Model.Repositories.Impl
{
    using Easytrade.Model.DbAccess;
    using Easytrade.Model.Domain.Bots;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISellOrderRepository : IOrderRepository<SellOrder> { }

    public class SellOrderRepository : ISellOrderRepository
    {
        private readonly EasyTradeDbContext _dbContext;

        public SellOrderRepository(EasyTradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SellOrder>> GetAllOrdersAsync()
        {
            var sellOrders = await _dbContext.SellOrders
                .AsNoTracking()
                .ToListAsync();

            return sellOrders;
        }

        public async Task<IEnumerable<SellOrder>> GetAllOrdersByBotIdAsync(long botId)
        {
            var sellOrders = await _dbContext.SellOrders
                .AsNoTracking()
                .Where(bo => bo.BotId == botId)
                .ToListAsync();

            return sellOrders;
        }

        public async Task<SellOrder> GetOrderByIdAsync(long id)
        {
            var sellOrder = await _dbContext.SellOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return sellOrder;
        }

        public async Task<SellOrder> CreateOrderAsync(SellOrder order)
        {
            await _dbContext.SellOrders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task DeleteOrderByIdAsync(long id)
        {
            var sellOrderToDelete = await _dbContext.SellOrders
                .FirstOrDefaultAsync(b => b.Id == id);

            if (sellOrderToDelete == null)
            {
                throw new ArgumentException($"Sell order with id: {id} not found in db!");
            }

            _dbContext.SellOrders.Remove(sellOrderToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SellOrder> UpdateOrderAsync(SellOrder updatedOrder)
        {
            _dbContext.SellOrders.Update(updatedOrder);
            await _dbContext.SaveChangesAsync();

            return updatedOrder;
        }

        public async Task<IEnumerable<SellOrder>> GetAllOpenOrdersAsync(long botId)
        {
            var sellOrders = await _dbContext.SellOrders
                .AsNoTracking()
                .Where(s => s.BotId == botId && s.Status == OrderStatus.Pending)
                .ToListAsync();

            return sellOrders;
        }
    }
}
