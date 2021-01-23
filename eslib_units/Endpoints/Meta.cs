using eslib.Endpoints;
using eslib.Models.Internals;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class MetaTests
    {
        private Mock<IDataService> _mockDataService;

        [SetUp]
        public void Init()
        {
            _mockDataService = new Mock<IDataService>();
        }

        [Test]
        public void Ping()
        {
            var mockResponse = new Response<string>() { Data = "ok" };

            // Ensure that the call to Get returns our mocked response.
            _mockDataService.Setup(m => m.Get<string>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            // Create our endpoint and call ping.
            var metaEndpoint = new MetaEndpoint(_mockDataService.Object);
            var result = metaEndpoint.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(mockResponse, result);
        }
    }
}
