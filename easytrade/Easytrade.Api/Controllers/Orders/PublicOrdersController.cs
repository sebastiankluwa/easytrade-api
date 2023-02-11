namespace Easytrade.Api.Controllers.Orders
{
    using Easytrade.Contract;
    using Easytrade.Contract.Requests.Orders;
    using Easytrade.Logic.Services;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    [Route(ApiContractUrlsV1.PublicOrders)]
    [SwaggerTag("Public")]
    [ApiController]
    public class PublicOrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public PublicOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("place")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<>))]
        public async Task<IActionResult> PlaceOrder([FromRoute] long botId, [FromBody] PlaceOrderRequest request)
        {
            var response = await _orderService.PlaceOrder(botId, request);

            return Ok(response);
        }

        [HttpPost("{id}/cancel")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<>))]
        public async Task<IActionResult> CancelOrder([FromRoute] long botId, [FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<>))]
        public async Task<IActionResult> GetOrder([FromRoute] long botId, [FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("open")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<>))]
        public async Task<IActionResult> GetOpenOrders([FromRoute] long botId)
        {
            return Ok();
        }

        //[HttpGet("symbols")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<>))]
        //public async Task<IActionResult> GetSymbols()
        //{
        //    return Ok();
        //}

        //[HttpPost("balances")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<>))]
        //public async Task<IActionResult> GetBalances()
        //{
        //    return Ok();
        //}
    }
}
