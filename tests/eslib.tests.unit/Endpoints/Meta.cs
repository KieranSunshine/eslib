using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Endpoints;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class MetaTests
    {
        private Mock<IDataService> _mockDataService;
        private Mock<IResponseFactory> _mockResponseFactory;
        
        [SetUp]
        public void Init()
        {
            _mockDataService = new Mock<IDataService>();
            _mockResponseFactory = new Mock<IResponseFactory>();
        }

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

            // Create our endpoint and call ping.
            var metaEndpoint = new MetaEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            var result = await metaEndpoint.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(response, result);
        }
    }
}