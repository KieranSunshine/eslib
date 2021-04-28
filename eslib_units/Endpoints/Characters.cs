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

        [Test]
        public async Task GetCorporationHistory()
        {
            var stubbedData = new[]
            {
                new CorporationHistory(1, 2, "2020-01-01"),
                new CorporationHistory(2, 3, "2020-01-01")
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<CorporationHistory[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<CorporationHistory[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.GetCorporationHistory(1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.AreEqual(stubbedData, result.Data);
        }

        [Test]
        public async Task CalculateCspa()
        {
            var stubbedData = 10.50;

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<double>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Post(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<double>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.CalculateCspa(1, new[] {0});
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.AreEqual(stubbedData, result.Data);
        }

        [Test]
        public async Task GetJumpFatigue()
        {
            var stubbedData = new JumpFatigue
            {
                ExpiryDate = "2020-01-01",
                LastJumpDate = "2020-01-02",
                LastUpdateDate = "2020-01-03"
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<JumpFatigue>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<JumpFatigue>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.GetJumpFatigue(1);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.AreEqual(stubbedData, result.Data);
        }

        [Test]
        public async Task GetMedals()
        {
            var stubbedData = new[]
            {
                new Medal(
                    "10",
                    "2020-01-01",
                    "Some description of a medal",
                    new[]
                    {
                        new Graphics("a graphic 1", 1, 1),
                        new Graphics("a graphic 2", 1, 1),
                        new Graphics("a graphic 3", 1, 1)
                    },
                    1,
                    123,
                    "for coming first",
                    Enums.Medals.Status.Private,
                    "medal owner"),
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<Medal[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Medal[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var characterEndpoint = new CharacterEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await characterEndpoint.GetMedals(1);
            
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            CollectionAssert.AreEqual(stubbedData, result.Data);
        }
    }
}