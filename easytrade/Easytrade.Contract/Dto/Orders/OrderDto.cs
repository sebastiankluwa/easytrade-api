namespace Easytrade.Contract.Dto.Orders
{
    using System;

    public enum OrderStatusDto
    {
        Pending = 0,
        Filled = 1,
        Canceled = 2
    }

    public enum OrderSideDto
    {
        Buy = 0,
        Sell = 1
    }

    public class OrderDto
    {
        public long Id { get; set; }

        public long BotId { get; set; }

        public long? ProfitLossId { get; set; }

        public long ReferenceOrderId { get; set; }

        public OrderSideDto Side { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public string Pair { get; set; }

        public decimal Rate { get; set; }

        public decimal Total { get; set; }

        public OrderStatusDto Status { get; set; }
    }
}
