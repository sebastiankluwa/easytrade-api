namespace Easytrade.Api.IntegrationTests.Controllers.Bots
{
    using Newtonsoft.Json;
    using System.Net;
    using Easytrade.Contract;
    using Easytrade.Contract.Dto.Bots;
    using Easytrade.Contract.Requests.Bots;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class BotsControllerTests : IntegrationApiTest
    {
        [Fact]
        public async void CreateBot_WhenCalledWithValidContent_ReturnsStatus201Created()
        {
            // Arrange
            var request = new CreateBotRequest
            {
                Name = "TestName",
                Allocation = 2000,
                IsActive = true,
                MaxOpenPositions = 20,
                MinimumAllocation = 15,
                Symbols = new[] { "BTCUSDT" }
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(ApiContractUrlsV1.Bots, content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void CreateBot_WhenCalledWithValidContent_ReturnsBodyThatCanBeDeserialized()
        {
            // Arrange
            var request = new CreateBotRequest
            {
                Name = "TestName",
                Allocation = 2000,
                IsActive = true,
                MaxOpenPositions = 20,
                MinimumAllocation = 15,
                Symbols = new[] { "BTCUSDT" }
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(ApiContractUrlsV1.Bots, content);
            var body = response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<BotDto>(body.Result);

            // Assert
            Assert.NotNull(deserializedResponse);
        }

        [Fact]
        public async void GetBot_WhenCalledWithValidId_ReturnsStatus200Ok()
        {
            // Arrange
            var request = new CreateBotRequest
            {
                Name = "TestName",
                Allocation = 2000,
                IsActive = true,
                MaxOpenPositions = 20,
                MinimumAllocation = 15,
                Symbols = new[] { "BTCUSDT" }
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var postResponse = await TestClient.PostAsync(ApiContractUrlsV1.Bots, content);
            var body = postResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<BotDto>(body.Result);

            // Act
            var response = await TestClient.GetAsync(ApiContractUrlsV1.Bots + "/" + deserializedResponse.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
