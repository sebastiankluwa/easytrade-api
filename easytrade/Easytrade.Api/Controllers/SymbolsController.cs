namespace Easytrade.Api.Controllers
{
    using Easytrade.Api.Binance.Logic.Repositories;
    using Easytrade.Contract;
    using Easytrade.Contract.Responses;
    using Microsoft.AspNetCore.Mvc;

    [Route(ApiContractUrlsV1.Symbols)]
    [ApiController]
    public class SymbolsController : ControllerBase
    {
        private readonly IBinanceRepository _binanceRepository;

        public SymbolsController(IBinanceRepository binanceRepository)
        {
            _binanceRepository = binanceRepository;
        }

        /// <summary>
        /// Returns all symbols
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetSymbolsResponse> GetSymbols()
        {
            var symbolsResponse = await _binanceRepository.GetSymbols();

            var response = new GetSymbolsResponse()
            {
                Data = symbolsResponse.ToArray()
            };

            return response;
        }
    }
}
