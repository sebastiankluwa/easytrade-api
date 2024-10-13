namespace Easytrade.Api.Binance.Logic.Repositories
{
    using Easytrade.Contract.Dto;
    using global::Binance.Net.Interfaces.Clients;
    using global::Binance.Net.Objects.Models.Spot.Margin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BinanceRepository : IBinanceRepository
    {
        private readonly IBinanceClient _binanceClient;

        public BinanceRepository(IBinanceClient binanceClient)
        {
            _binanceClient = binanceClient;
        }

        public async Task<IEnumerable<SymbolDto>> GetSymbols()
        {
            var getSymbolsTask = _binanceClient.SpotApi.ExchangeData.GetMarginSymbolsAsync();
            var getAssetsTask = _binanceClient.SpotApi.ExchangeData.GetMarginAssetsAsync();

            await Task.WhenAll(getSymbolsTask, getAssetsTask);

            var symbolPairs = await getSymbolsTask;

            var assets = await getAssetsTask;
            var assetsData = assets.Data.ToArray();

            return symbolPairs.Data.Select(sp => new SymbolDto(
                sp.Symbol,
                GetSymbolDescription(sp.BaseAsset, sp.QuoteAsset, assetsData),
                sp.BaseAsset,
                sp.QuoteAsset
                ));
        }

        private static string GetSymbolDescription(string baseAssetCode, string quoteAssetCode,
            BinanceMarginAsset[] assets)
        {
            var baseAssetName = assets
                .FirstOrDefault(a => a.Name.Equals(baseAssetCode))?.FullName ?? baseAssetCode;

            var quoteAssetName = assets
                .FirstOrDefault(a => a.Name.Equals(quoteAssetCode))?.FullName ?? quoteAssetCode;

            return $"{baseAssetName} / {quoteAssetName}";
        }
    }
}
