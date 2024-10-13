using Easytrade.Contract.Dto.Orders;

namespace Easytrade.Api.Controllers.Orders
{
    using Easytrade.Contract;
    using Easytrade.Logic.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    [Route(ApiContractUrlsV1.Orders)]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICompositeOrdersRepository _compositeOrdersRepository;

        public OrdersController(ICompositeOrdersRepository compositeOrdersRepository)
        {
            _compositeOrdersRepository = compositeOrdersRepository;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IActionResult> GetOrdersForBot([FromRoute] long botId)
        {
            var response = await _compositeOrdersRepository.GetAllOrdersByBotId(botId);

            return Ok(response);
        }
    }
}
