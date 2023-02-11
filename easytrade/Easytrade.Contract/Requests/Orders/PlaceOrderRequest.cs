namespace Easytrade.Contract.Requests.Orders
{
    using Easytrade.Contract.Dto.Orders;
    using System.ComponentModel.DataAnnotations;

    public class PlaceOrderRequest
    {
        [Required]
        public string Pair { get; set; }

        [Required]
        public OrderSideDto Side { get; set; }

        [Required]
        public decimal Rate { get; set; }
    }
}
