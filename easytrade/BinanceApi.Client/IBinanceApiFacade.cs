using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using Easytrade.Contract.Dto;

namespace BinanceApi.Client
{
    public interface IBinanceApiFacade
    {
        Task<IEnumerable<SymbolDto>> GetSymbols();

        Task<BinancePlacedOrder> PlaceOrder(string symbol, 
            string side, 
            decimal quantity, 
            decimal price,
            SpotOrderType orderType = SpotOrderType.Limit);
    }
}
