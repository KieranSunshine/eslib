using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using eslib;
using eslib.Endpoints;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;
using Moq;
using NUnit.Framework;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class CharacterTests
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
        public async Task GetCharacterTest()
        {
            var stubbedData = new Character(
                "2020-01-01",
                1,
                1,
                Enums.Character.Gender.Female,
                "Jane Doe",
                2);

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<Character>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Character>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.GetCharacter(1);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            Assert.IsNull(result.Error);
            Assert.AreEqual(stubbedData, result.Data);
        }

        [Test]
        public async Task GetAgentsResearchTest()
        {
            var stubbedData = new[]
            {
                new AgentResearch(
                    1,
                    5,
                    5,
                    1,
                    "12:00")
            };

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<AgentResearch[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<AgentResearch[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.GetAgentsResearch(1);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.AreEqual(stubbedData, result.Data);
        }

        [Test]
        public async Task GetBlueprintsTest()
        {
            var stubbedData = new[]
            {
                new Blueprint(
                    123456789,
                    Enums.Locations.LocationFlags.Cargo,
                    123456789,
                    20,
                    5,
                    5,
                    20,
                    1234567),
                
                new Blueprint(
                    123456789,
                    Enums.Locations.LocationFlags.Deliveries,
                    123456789,
                    15,
                    2,
                    6,
                    15,
                    1234567)
            };

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<Blueprint[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Blueprint[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.GetBlueprints(1);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.AreEqual(stubbedData, result.Data);
        }
    }
}