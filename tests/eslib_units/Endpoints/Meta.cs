using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using eslib.Endpoints;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;
using Moq;
using NUnit.Framework;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class MetaTests
    {
        [SetUp]
        public void Init()
        {
            _stubbedRequest = new Request();

            _mockRequestFactory = new Mock<IRequestFactory>();
            _mockDataService = new Mock<IDataService>();
            _mockResponseFactory = new Mock<IResponseFactory>();

            _mockRequestFactory
                .Setup(m => m.Create())
                .Returns(_stubbedRequest);
        }

        private Request _stubbedRequest;

        private Mock<IRequestFactory> _mockRequestFactory;
        private Mock<IDataService> _mockDataService;
        private Mock<IResponseFactory> _mockResponseFactory;

        [Test]
        public async Task Ping()
        {
            var data = "ok";

            var httpResponse = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(data))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<string>(HttpStatusCode.OK, "ok");

            // Ensure that the call to Get returns our mocked response.
            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            // Create our endpoint and call ping.
            var metaEndpoint = new MetaEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await metaEndpoint.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(response, result);
        }
    }
}