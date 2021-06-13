using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Endpoints;
using Eslib.Models.Internals;
using Flurl;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class MetaTests : EndpointTestBase<MetaEndpoint>
    {
        [Test]
        public async Task Ping()
        {
            var data = "ok";

            var httpResponse = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(data))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<string>(HttpStatusCode.OK, "ok");

            // Ensure that the call to Get returns our mocked response.
            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var result = await Target.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(response, result);
        }
    }
}