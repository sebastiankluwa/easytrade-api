namespace Easytrade.Api.Controllers.Bots
{
    using AutoMapper;
    using Easytrade.Contract;
    using Easytrade.Contract.Dto.Bots;
    using Easytrade.Contract.Requests.Bots;
    using Easytrade.Model.Domain.Bots;
    using Easytrade.Model.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Net;
    using System.Threading.Tasks;

    [Route(ApiContractUrlsV1.Bots)]
    [ApiController]
    public class BotsController : ControllerBase
    {
        private readonly IBotRepository _botRepository;
        private readonly IMapper _mapper;

        public BotsController(IBotRepository botRepository, IMapper mapper)
        {
            _botRepository = botRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<BotDto>))]
        public async Task<IActionResult> GetBots()
        {
            var botEntities = await _botRepository.GetAllBotsAsync();
            var response = _mapper.Map<IEnumerable<BotDto>>(botEntities);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BotDto))]
        public async Task<IActionResult> GetBot([FromRoute] long id)
        {
            var botEntity = await _botRepository.GetBotByIdAsync(id);
            var response = _mapper.Map<BotDto>(botEntity);

            return Ok(response);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BotDto))]
        public async Task<IActionResult> CreateBot([FromBody] CreateBotRequest request)
        {
            var botToCreate = _mapper.Map<Bot>(request);

            var botEntity = await _botRepository.CreateBotAsync(botToCreate);

            var response = _mapper.Map<BotDto>(botEntity);

            return CreatedAtAction(nameof(GetBot), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(BotDto))]
        public async Task<IActionResult> UpdateBot([FromRoute] long id, [FromBody] UpdateBotRequest request)
        {
            var bot = await _botRepository.GetBotByIdAsync(id);

            if (bot == null)
            {
                throw new KeyNotFoundException($"Bot {id} not found!");
            }

            _mapper.Map(request, bot);

            var updatedBot = await _botRepository.UpdateBotAsync(bot);

            var response = _mapper.Map<BotDto>(updatedBot);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBot([FromRoute] long id)
        {
            await _botRepository.DeleteBotByIdAsync(id);

            return NoContent();
        }
    }
}
