namespace Easytrade.Contract.Dto.Orders
{
    using System;

    public class ProfitLossDto
    {
        public long Id { get; set; }

        public long BuyOrderId { get; set; }

        public long SellOrderId { get; set; }

        public decimal? Result { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}
