using eslib.Endpoints;
using eslib.Models;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Text.Json;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    public class AllianceTests
    {
        [Test]
        public void GetAllianceIds()
        {
            var mock = new Mock<IDataService>();
            var mockResponse = Task.FromResult(new Response<int[]>() { data = new int[100] });

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("somestring");

            mock.Setup(m => m.Fetch<int[]>(It.IsAny<string>()))
                .Returns(mockResponse);

            var allianceEndpoint = new AllianceEndpoint(mock.Object);
            var result = allianceEndpoint.GetAllianceIds();

            Assert.AreEqual(result, mockResponse.Result.data);
        }

        [Test]
        public void GetAlliance()
        {
            var mock = new Mock<IDataService>();

            var expectedObject = new Alliance()
            {
                CreatorCorporationId = 1,
                CreatorId = 2,
                DateFounded  = new System.DateTime(2021, 1, 3),
                ExecutorCorporationId = 3,
                FactionId = 4,
                Name = "test",
                Ticker = "test"
            };
            var mockResponse = Task.FromResult(new Response<Alliance>() { data = expectedObject });

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("somestring/someparam");

            mock.Setup(m => m.Fetch<Alliance>(It.IsAny<string>()))
                .Returns(mockResponse);

            var allianceEndpoint = new AllianceEndpoint(mock.Object);
            var result = allianceEndpoint.GetAlliance(1);

            Assert.AreEqual(mockResponse.Result.data, result);
        }
    }
}
