namespace Easytrade.Api.Controllers.Orders
{
    using Easytrade.Contract;
    using Easytrade.Contract.Dto.Orders;
    using Easytrade.Contract.Requests.Orders;
    using Easytrade.Logic.Repositories;
    using Easytrade.Logic.Services;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    [Route(ApiContractUrlsV1.PublicOrders)]
    [SwaggerTag("Public")]
    [ApiController]
    public class PublicOrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICompositeOrdersRepository _compositeOrdersRepository;

        public PublicOrdersController(IOrderService orderService,
            ICompositeOrdersRepository compositeOrdersRepository)
        {
            _orderService = orderService;
            _compositeOrdersRepository = compositeOrdersRepository;
        }

        [HttpPost("place")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IActionResult> PlaceOrder([FromRoute] long botId, [FromBody] PlaceOrderRequest request)
        {
            var response = await _orderService.PlaceOrder(botId, request);

            return Ok(response);
        }

        [HttpPost("cancel")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CancelOrder([FromRoute] long botId, [FromQuery] long orderId)
        {
            await _orderService.CancelOrder(botId, orderId);

            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OrderDto))]
        public async Task<IActionResult> GetOrder([FromRoute] long botId, [FromRoute] long id)
        {
            var response = await _compositeOrdersRepository.GetOrder(id);

            return Ok(response);
        }

        [HttpGet("open")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IActionResult> GetOpenOrders([FromRoute] long botId)
        {
            var response = await _compositeOrdersRepository.GetAllOpenOrdersByBotId(botId);

            return Ok(response);
        }
    }
}
