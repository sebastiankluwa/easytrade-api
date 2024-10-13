namespace Easytrade.Model.Domain.Bots
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProfitsLosses")]
    public class ProfitLoss
    {
        public long Id { get; set; }

        public long BuyOrderId { get; set; }

        public long SellOrderId { get; set; }

        public decimal? Result { get; set; }

        public DateTime? CompletionDate { get; set; }

        public BuyOrder BuyOrder { get; set; }

        public SellOrder SellOrder { get; set; }
    }
}
