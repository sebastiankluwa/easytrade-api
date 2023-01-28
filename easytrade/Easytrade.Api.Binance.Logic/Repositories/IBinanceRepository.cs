namespace Easytrade.Api.Binance.Logic.Repositories
{
    using Easytrade.Contract.Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBinanceRepository
    {
        Task<IEnumerable<SymbolDto>> GetSymbols();
    }
}
