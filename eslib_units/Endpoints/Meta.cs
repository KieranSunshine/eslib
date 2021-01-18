using eslib.Endpoints;
using eslib.Models;
using eslib.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    public class MetaTests
    {
        [Test]
        public void Ping()
        {
            var mock = new Mock<IDataService>();
            var mockResponse = new Response<string>() { Data = "ok" };

            // Ensure that the call to Get returns our mocked response.
            mock.Setup(m => m.Get<string>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            // Create our endpoint and call ping.
            var metaEndpoint = new MetaEndpoint(mock.Object);
            var result = metaEndpoint.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(mockResponse, result);
        }
    }
}
