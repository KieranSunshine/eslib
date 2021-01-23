using eslib.Endpoints;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class AllianceTests
    {

        private Mock<IDataService> mockDataService { get; set; }

        [SetUp]
        public void Init() 
        {
            mockDataService = new Mock<IDataService>();

            mockDataService.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("somestring");
        }

        [Test]
        public void GetAllianceIds()
        {
            var mockResponse = new Response<int[]>() { Data = new int[100] };
            
            mockDataService.Setup(m => m.Get<int[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mockDataService.Object);
            var result = allianceEndpoint.GetAllianceIds();

            Assert.AreEqual(mockResponse, result);
        }

        [Test]
        public void GetAlliance()
        {
            var expectedObject = new Alliance(1, 2, new System.DateTime(2021, 1, 3), "test", "test")
            {
                ExecutorCorporationId = 3,
                FactionId = 4
            };
            var mockResponse = new Response<Alliance>() { Data = expectedObject };

            mockDataService.Setup(m => m.Get<Alliance>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mockDataService.Object);
            var result = allianceEndpoint.GetAlliance(1);

            Assert.AreEqual(mockResponse, result);
        }

        [Test]
        public void GetAllianceCorporationIds()
        {
            var mockResponse = new Response<int[]>() { Data = new int[100] };

            mockDataService.Setup(m => m.Get<int[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mockDataService.Object);
            var result = allianceEndpoint.GetAllianceCorporationIds(1);

            Assert.AreEqual(mockResponse, result);
        }

        [Test]
        public void GetAllianceIcon()
        {
            var expectedObject = new Icon()
            {
                Url128px = "some/128px/url",
                Url64px = "some/64px/url"
            };
            var mockResponse = new Response<Icon>() { Data = expectedObject };

            mockDataService.Setup(m => m.Get<Icon>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mockDataService.Object);
            var result = allianceEndpoint.GetAllianceIcon(1);

            Assert.AreEqual(mockResponse, result);
        }
    }
}
