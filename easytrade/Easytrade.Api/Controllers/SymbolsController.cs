namespace Easytrade.Api.Controllers
{
    using BinanceApi.Client;
    using Easytrade.Contract;
    using Easytrade.Contract.Responses;
    using Microsoft.AspNetCore.Mvc;

    [Route(ApiContractUrlsV1.Symbols)]
    [ApiController]
    public class SymbolsController : ControllerBase
    {
        private readonly IBinanceApiFacade _binanceApiFacade;

        public SymbolsController(IBinanceApiFacade binanceApiFacade)
        {
            _binanceApiFacade = binanceApiFacade;
        }

        /// <summary>
        /// Returns all symbols
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetSymbolsResponse> GetSymbols()
        {
            var symbolsResponse = await _binanceApiFacade.GetSymbols();

            var response = new GetSymbolsResponse()
            {
                Data = symbolsResponse.ToArray()
            };

            return response;
        }
    }
}
