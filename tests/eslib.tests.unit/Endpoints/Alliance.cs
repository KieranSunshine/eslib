using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Endpoints;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class AllianceTests
    {
        [SetUp]
        public void Init()
        {
            _stubbedEsiRequest = new EsiRequest();

            _mockDataService = new Mock<IDataService>();
            _mockRequestFactory = new Mock<IRequestFactory>();
            _mockResponseFactory = new Mock<IResponseFactory>();

            _mockRequestFactory.Setup(m => m.Create())
                .Returns(_stubbedEsiRequest);
        }

        private EsiRequest _stubbedEsiRequest;

        private Mock<IDataService> _mockDataService;
        private Mock<IRequestFactory> _mockRequestFactory;
        private Mock<IResponseFactory> _mockResponseFactory;

        [Test]
        public async Task GetAllianceIds()
        {
            var stubbedData = new int[100];

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<int[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<EsiRequest>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<int[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await allianceEndpoint.GetAllianceIds();

            Assert.AreEqual(response, result);
        }

        [Test]
        public async Task GetAlliance()
        {
            var stubbedData = new Alliance(
                1,
                2,
                new DateTime(2021, 1, 3),
                "test",
                "test")
            {
                ExecutorCorporationId = 3,
                FactionId = 4
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<Alliance>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<EsiRequest>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Alliance>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await allianceEndpoint.GetAlliance(1);

            Assert.AreEqual(response, result);
        }

        [Test]
        public async Task GetAllianceCorporationIds()
        {
            var stubbedData = new int[100];

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<int[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<EsiRequest>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<int[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await allianceEndpoint.GetAllianceCorporationIds(1);

            Assert.AreEqual(response, result);
        }

        [Test]
        public async Task GetAllianceIcon()
        {
            var stubbedData = new Icon
            {
                Url128 = "some/128px/url",
                Url64 = "some/64px/url"
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<Icon>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<EsiRequest>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Icon>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var allianceEndpoint = new AllianceEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await allianceEndpoint.GetAllianceIcon(1);

            Assert.AreEqual(response, result);
        }
    }
}