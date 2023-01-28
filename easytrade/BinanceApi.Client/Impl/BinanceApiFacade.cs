namespace BinanceApi.Client.Impl
{
    using Binance.Net.Clients;
    using System.Linq;
    using System.Threading.Tasks;

    public class BinanceApiFacade : IBinanceApiFacade
    {
        private readonly BinanceClient _binanceClient;

        public BinanceApiFacade(BinanceClient client)
        {
            _binanceClient = client;
        }

        public async Task<> GetPairs()
        {
            var pairs = await _binanceClient.SpotApi.ExchangeData.GetMarginSymbolsAsync();

            var assets = await _binanceClient.SpotApi.ExchangeData.GetMarginAssetsAsync();

            var pairsAssets = pairs.Data.SelectMany(p => new[] { p.QuoteAsset, p.BaseAsset });

            
        }
    }
}
