using eslib.Endpoints;
using eslib.Models;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class MetaTests
    {
        private Mock<IDataService> mockDataService { get; set; }

        [SetUp]
        public void Init()
        {
            mockDataService = new Mock<IDataService>();
        }

        [Test]
        public void Ping()
        {
            var mockResponse = new Response<string>() { Data = "ok" };

            // Ensure that the call to Get returns our mocked response.
            mockDataService.Setup(m => m.Get<string>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            // Create our endpoint and call ping.
            var metaEndpoint = new MetaEndpoint(mockDataService.Object);
            var result = metaEndpoint.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(mockResponse, result);
        }
    }
}
