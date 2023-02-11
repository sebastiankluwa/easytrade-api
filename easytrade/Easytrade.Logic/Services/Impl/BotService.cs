namespace Easytrade.Logic.Services.Impl
{
    using Easytrade.Model.Repositories;

    public class BotService : IBotService
    {
        private readonly IBotRepository _botRepository;

        public BotService(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }


    }
}
