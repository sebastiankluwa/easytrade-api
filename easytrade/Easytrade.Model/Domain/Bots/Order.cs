namespace Easytrade.Model.Domain.Bots
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public enum OrderStatus
    {
        Pending = 0,
        Filled = 1,
        Canceled = 2
    }

    public class Order
    {
        public long Id { get; set; }

        public long BotId { get; set; }

        public Bot Bot { get; set; }

        public long? ProfitLossId { get; set; }

        public ProfitLoss ProfitLoss { get; set; }

        [Required]
        public long ReferenceOrderId { get; set; }

        [Required]
        public string Side { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal? Fee { get; set; }

        [Required]
        public string Pair { get; set; }

        [Required]
        public decimal Rate { get; set; }

        public decimal Total => Rate * Amount;

        public OrderStatus Status { get; set; }
    }
}
