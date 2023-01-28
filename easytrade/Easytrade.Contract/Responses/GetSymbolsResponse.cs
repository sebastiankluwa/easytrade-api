namespace Easytrade.Contract.Responses
{
    using Easytrade.Contract.Dto;
    using System;

    public class GetSymbolsResponse
    {
        public SymbolDto[] Data { get; set; } = Array.Empty<SymbolDto>();
    }
}
