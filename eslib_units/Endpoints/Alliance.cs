using System.Net.Http;
using System.Text.Json;
using eslib.Endpoints;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using eslib.Services.Factories;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class AllianceTests
    {
        private Request _stubbedRequest;
        
        private Mock<IDataService> _mockDataService;
        private Mock<IRequestFactory> _mockRequestFactory;
        private Mock<IResponseFactory> _mockResponseFactory;

        [SetUp]
        public void Init() 
        {
            _stubbedRequest = new Request();
            
            _mockDataService = new Mock<IDataService>();
            _mockRequestFactory = new Mock<IRequestFactory>();
            _mockResponseFactory = new Mock<IResponseFactory>();
            
            _mockRequestFactory.Setup(m => m.Create())
                .Returns(_stubbedRequest);
        }

        [Test]
        public void GetAllianceIds()
        {
            var stubbedData = new int[100];

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<int[]>() { Data = stubbedData };

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<int[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);
            var result = allianceEndpoint.GetAllianceIds();

            Assert.AreEqual(response, result);
        }

        [Test]
        public void GetAlliance()
        {
            var stubbedData = new Alliance(
                1,
                2,
                new System.DateTime(2021, 1, 3),
                "test",
                "test")
            {
                ExecutorCorporationId = 3,
                FactionId = 4
            };

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<Alliance>() { Data = stubbedData };

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Alliance>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                 _mockRequestFactory.Object,
                _mockResponseFactory.Object);
            
            var result = allianceEndpoint.GetAlliance(1);

            Assert.AreEqual(response, result);
        }

        [Test]
        public void GetAllianceCorporationIds()
        {
            var stubbedData = new int[100];

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<int[]>() { Data = stubbedData };

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<int[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);
            
            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);
            var result = allianceEndpoint.GetAllianceCorporationIds(1);

            Assert.AreEqual(response, result);
        }

        [Test]
        public void GetAllianceIcon()
        {
            var stubbedData = new Icon()
            {
                Url128px = "some/128px/url",
                Url64px = "some/64px/url"
            };

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<Icon>() { Data = stubbedData };

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Icon>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);
            var result = allianceEndpoint.GetAllianceIcon(1);

            Assert.AreEqual(response, result);
        }
    }
}
