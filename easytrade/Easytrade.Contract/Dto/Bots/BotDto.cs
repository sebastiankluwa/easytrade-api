namespace Easytrade.Contract.Dto.Bots
{
    using Easytrade.Contract.Dto.Orders;
    using System.Collections.Generic;

    public class BotDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Allocation { get; set; }

        public decimal? TotalProfit { get; set; }

        public decimal? DayProfit { get; set; }

        public bool IsActive { get; set; }

        public string[] Symbols { get; set; }

        public int MaxOpenPositions { get; set; }

        public decimal MinimumAllocation { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}
