namespace Easytrade.Api.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Api;

    public class IntegrationApiTest
    {
        protected readonly HttpClient TestClient;

        public IntegrationApiTest()
        {
            var appFactory = new ApiFactory<Program>("easytrade");
            TestClient = appFactory.CreateClient();
        }
    }
}
