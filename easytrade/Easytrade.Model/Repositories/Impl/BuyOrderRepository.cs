namespace Easytrade.Model.Repositories.Impl
{
    using Easytrade.Model.DbAccess;
    using Easytrade.Model.Domain.Bots;
    using Easytrade.Model.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBuyOrderRepository : IOrderRepository<BuyOrder>
    {
        Task<BuyOrder> GetOldestUnmatchedBuyOrderForBot(long botId);
        Task<int> CountAllOpenBuyOrdersForBot(long botId);
        Task<decimal> SumOpenBuyOrdersAllocationForBot(long botId);
    }

    public class BuyOrderRepository : IBuyOrderRepository
    {
        private readonly EasyTradeDbContext _dbContext;

        public BuyOrderRepository(EasyTradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuyOrder> GetOldestUnmatchedBuyOrderForBot(long botId)
        {
            var buyOrder = await _dbContext.BuyOrders
                .Where(b => b.BotId == botId
                            && b.ProfitLoss == null
                            && b.Status.Equals(OrderStatus.Filled))
                .OrderBy(b => b.OrderDate)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return buyOrder;
        }

        public async Task<int> CountAllOpenBuyOrdersForBot(long botId)
        {
            var openBuyOrdersCount = await _dbContext.BuyOrders
                .Where(b => b.BotId == botId && b.Status.Equals(OrderStatus.Pending))
                .AsNoTracking()
                .CountAsync();

            return openBuyOrdersCount;
        }

        public async Task<decimal> SumOpenBuyOrdersAllocationForBot(long botId)
        {
            var allocationSum = await _dbContext.BuyOrders
                .Where(b => b.BotId == botId && b.Status.Equals(OrderStatus.Pending))
                .AsNoTracking()
                .SumAsync(b => b.Amount);

            return allocationSum;
        }


        public async Task<IEnumerable<BuyOrder>> GetAllOrdersAsync()
        {
            var buyOrders = await _dbContext.BuyOrders
                .AsNoTracking()
                .ToListAsync();

            return buyOrders;
        }

        public async Task<IEnumerable<BuyOrder>> GetAllOrdersByBotIdAsync(long botId)
        {
            var buyOrders = await _dbContext.BuyOrders
                .AsNoTracking()
                .Where(bo => bo.BotId == botId)
                .ToListAsync();

            return buyOrders;
        }

        public async Task<BuyOrder> GetOrderByIdAsync(long id)
        {
            var buyOrder = await _dbContext.BuyOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return buyOrder;
        }

        public async Task<BuyOrder> CreateOrderAsync(BuyOrder order)
        {
            await _dbContext.BuyOrders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task DeleteOrderByIdAsync(long id)
        {
            var buyOrderToDelete = await _dbContext.BuyOrders
                .FirstOrDefaultAsync(b => b.Id == id);

            if (buyOrderToDelete == null)
            {
                throw new ArgumentException($"Buy order with id: {id} not found in db!");
            }

            _dbContext.BuyOrders.Remove(buyOrderToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BuyOrder> UpdateOrderAsync(BuyOrder updatedOrder)
        {
            _dbContext.BuyOrders.Update(updatedOrder);
            await _dbContext.SaveChangesAsync();

            return updatedOrder;
        }
    }
}
