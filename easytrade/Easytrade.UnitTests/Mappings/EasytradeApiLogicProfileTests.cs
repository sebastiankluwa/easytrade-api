using Easytrade.Model.Domain.Bots;
using Easytrade.UnitTests.Utils;

namespace Easytrade.UnitTests.Mappings
{
    using AutoMapper;
    using Easytrade.Contract.Dto.Bots;
    using Easytrade.Logic.Mappings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class EasytradeApiLogicProfileTests
    {
        private readonly IMapper _mapper;

        public EasytradeApiLogicProfileTests()
        {

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new EasytradeApiLogicProfile()));

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void MapBot_ReturnsProperlyMappedBotDto()
        {
            var bot = SampleDataReader.GetTestObjectFromResource<Bot>("Easytrade.UnitTests.TestData.Bot_1.json");

            var botDto = _mapper.Map<BotDto>(bot);

            Assert.NotNull(botDto);
            Assert.Equal(100000.0M, botDto.Allocation);
            Assert.True(botDto.IsActive);
            Assert.Equal("MACD Bot", botDto.Name);
            Assert.True(botDto.Symbols.Length == 1);
        }

        [Fact]
        public void MapBot_WithOrders_ReturnsProperlyMappedBotDto()
        {
            var bot = SampleDataReader.GetTestObjectFromResource<Bot>("Easytrade.UnitTests.TestData.Bot_1.json");

            var botDto = _mapper.Map<BotDto>(bot);

            Assert.NotNull(botDto);
            Assert.True(botDto.Orders.Count == 4);
            Assert.NotNull(botDto.Orders.FirstOrDefault()?.ProfitLoss);
        }
    }
}
