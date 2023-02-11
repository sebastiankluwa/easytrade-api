namespace Easytrade.Contract.Dto.Bots
{
    using System.Collections.Generic;

    public class BotDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Allocation { get; set; }

        public decimal? TotalProfit { get; set; }

        public decimal? DayProfit { get; set; }

        public bool IsActive { get; set; }

        public List<string> Symbols { get; set; }

        public int MaxOpenPositions { get; set; }

        public decimal MinimumAllocation { get; set; }
    }
}
