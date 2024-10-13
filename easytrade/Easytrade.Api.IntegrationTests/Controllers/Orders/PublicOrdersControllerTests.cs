using Easytrade.Contract.Requests.Orders;
using Xunit;

namespace Easytrade.Api.IntegrationTests.Controllers.Orders
{
    using Easytrade.Contract;
    using Easytrade.Contract.Dto.Orders;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class PublicOrdersControllerTests : IntegrationApiTest
    {
        [Fact]
        public async void GetOrder_WhenCalledWithValidId_ReturnsStatus200Ok()
        {
            // Arrange
            var request = new PlaceOrderRequest
            {

                Pair = "BTCUSDT",
                Rate = (decimal)0.0028,
                Side = OrderSideDto.Buy
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var postResponse = await TestClient.PostAsync(string.Format(ApiContractUrlsV1.PublicOrders, "1") + "/place", content);
            var body = postResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<OrderDto>(body.Result);

            // Act
            var response = await TestClient.GetAsync(string.Format(ApiContractUrlsV1.PublicOrders, "1") + "/" + deserializedResponse.Id);

            var emptyContent = new StringContent("", Encoding.UTF8, "application/json");
            await TestClient.PostAsync(string.Format(ApiContractUrlsV1.PublicOrders, "1") + "/cancel/" + deserializedResponse.Id, emptyContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
