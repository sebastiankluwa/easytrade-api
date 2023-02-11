namespace Easytrade.Model.Repositories.Impl
{
    using Easytrade.Model.DbAccess;
    using Easytrade.Model.Domain.Bots;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class BotRepository : IBotRepository

    {
        private readonly EasyTradeDbContext _dbContext;

        public BotRepository(EasyTradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Bot>> GetAllBotsAsync()
        {
            var bots = await _dbContext.Bots
                .AsNoTracking()
                .ToListAsync();

            return bots;
        }

        public async Task<Bot> GetBotByIdAsync(long botId)
        {
            var bot = await _dbContext.Bots
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == botId);

            return bot;
        }

        public async Task<Bot> CreateBotAsync(Bot bot)
        {
            await _dbContext.Bots.AddAsync(bot);
            await _dbContext.SaveChangesAsync();

            return bot;
        }

        public async Task DeleteBotByIdAsync(long botId)
        {
            var botToDelete = await _dbContext.Bots
                .FirstOrDefaultAsync(b => b.Id == botId);

            if (botToDelete == null)
            {
                throw new ArgumentException($"Bot with id: {botId} not found in db!");
            }

            _dbContext.Bots.Remove(botToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Bot> UpdateBotAsync(Bot updatedBot)
        {
            _dbContext.Bots.Update(updatedBot);
            await _dbContext.SaveChangesAsync();

            return updatedBot;
        }
    }
}
