namespace Easytrade.Model.Repositories
{
    using Easytrade.Model.Domain.Bots;

    public interface IBotRepository
    {
        Task<Bot> GetBotByIdAsync(long botId);
        Task<Bot> CreateBotAsync(Bot bot);
        Task DeleteBotByIdAsync(long botId);
        Task<IEnumerable<Bot>> GetAllBotsAsync();
        Task<Bot> UpdateBotAsync(Bot updatedBot);
    }
}
