namespace Easytrade.Contract.Dto
{
    public class SymbolDto
    {
        public SymbolDto()
        {
            
        }

        public SymbolDto(string name, string description, string baseAssetCode, string quoteAssetCode)
        {
            Name = name;
            Description = description;
            BaseAssetCode = baseAssetCode;
            QuoteAssetCode = quoteAssetCode;
            BaseAssetUrl = $"https://s3-symbol-logo.tradingview.com/crypto/XTVC{baseAssetCode.ToUpper()}.svg";
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string BaseAssetCode { get; set; }

        public string QuoteAssetCode { get; set; }

        public string BaseAssetUrl { get; set; }
    }
}
