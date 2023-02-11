namespace BinanceApi.Client.Impl
{
    using Binance.Net.Clients;
    using Binance.Net.Enums;
    using Binance.Net.Interfaces.Clients;
    using Binance.Net.Objects.Models.Spot;
    using Binance.Net.Objects.Models.Spot.Margin;
    using Easytrade.Contract.Dto;
    using System.Linq;
    using System.Threading.Tasks;

    public class BinanceApiFacade : IBinanceApiFacade
    {
        private readonly IBinanceClient _binanceClient;

        public BinanceApiFacade(IBinanceClient client)
        {
            _binanceClient = client;
        }

        public async Task<BinancePlacedOrder> PlaceOrder(string symbol, 
            string side, 
            decimal quantity, 
            decimal price,
            SpotOrderType orderType = SpotOrderType.Limit)
        {
            if (!Enum.TryParse<OrderSide>(side, true, out var orderSide))
            {
                throw new ArgumentException("Bad order side provided.");
            }

            var callResult = await _binanceClient.SpotApi.Trading.PlaceOrderAsync(
                symbol,
                orderSide,
                orderType,
                quantity: quantity,
                price: price,
                timeInForce: TimeInForce.GoodTillCanceled);

            if (!callResult.Success)
            {
                throw new InvalidOperationException(
                    $"Occur errors during call Binance API. Error code: {callResult.Error?.Code}. Details: {callResult.Error?.Message}");
            }

            return callResult.Data;
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
