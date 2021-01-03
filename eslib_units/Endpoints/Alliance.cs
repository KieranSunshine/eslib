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
            var mockResponse = Task.FromResult(new Response<int[]>() { data = new int[100] });

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("somestring");

            mock.Setup(m => m.Fetch<int[]>(It.IsAny<string>()))
                .Returns(mockResponse);

            var allianceEndpoint = new Alliance(mock.Object);
            var result = allianceEndpoint.GetAllianceIds();

            Assert.AreEqual(result, mockResponse.Result.data);
        }
    }
}
