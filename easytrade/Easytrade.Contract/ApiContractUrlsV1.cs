namespace Easytrade.Contract
{
    public static class ApiContractUrlsV1
    {
        public const string Version = "v1";

        public const string PublicApiVersion = "public/api/" + Version;

        public const string InternalApiVersion = "api/" + Version;

        public const string Symbols = InternalApiVersion + "/symbols";

        public const string Bots = InternalApiVersion + "/bots";

        public const string PublicOrders = PublicApiVersion + "/{botId}/orders";

        public const string Orders = InternalApiVersion + "/{botId}/orders";
    }
}
