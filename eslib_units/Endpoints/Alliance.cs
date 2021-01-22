using eslib.Endpoints;
using eslib.Models;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    public class AllianceTests
    {
        [Test]
        public void GetAllianceIds()
        {
            var mock = new Mock<IDataService>();
            var mockResponse = new Response<int[]>() { Data = new int[100] };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("somestring");

            mock.Setup(m => m.Get<int[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mock.Object);
            var result = allianceEndpoint.GetAllianceIds();

            Assert.AreEqual(mockResponse, result);
        }

        [Test]
        public void GetAlliance()
        {
            var mock = new Mock<IDataService>();

            var expectedObject = new Alliance("test", "test")
            {
                CreatorCorporationId = 1,
                CreatorId = 2,
                DateFounded  = new System.DateTime(2021, 1, 3),
                ExecutorCorporationId = 3,
                FactionId = 4
            };
            var mockResponse = new Response<Alliance>() { Data = expectedObject };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("somestring/someparam");

            mock.Setup(m => m.Get<Alliance>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mock.Object);
            var result = allianceEndpoint.GetAlliance(1);

            Assert.AreEqual(mockResponse, result);
        }

        [Test]
        public void GetAllianceCorporationIds()
        {
            var mock = new Mock<IDataService>();
            var mockResponse = new Response<int[]>() { Data = new int[100] };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("somestring");

            mock.Setup(m => m.Get<int[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mock.Object);
            var result = allianceEndpoint.GetAllianceCorporationIds(1);

            Assert.AreEqual(mockResponse, result);
        }

        [Test]
        public void GetAllianceIcon()
        {
            var mock = new Mock<IDataService>();

            var expectedObject = new Icon()
            {
                Url128px = "some/128px/url",
                Url64px = "some/64px/url"
            };
            var mockResponse = new Response<Icon>() { Data = expectedObject };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("somestring/someparam");

            mock.Setup(m => m.Get<Icon>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var allianceEndpoint = new AllianceEndpoint(mock.Object);
            var result = allianceEndpoint.GetAllianceIcon(1);

            Assert.AreEqual(mockResponse, result);
        }
    }
}
