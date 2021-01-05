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

            var mockResponse = Task.FromResult(new Response<string>() { data = "ok" });

            // Ensure that the call to fetch returns our mocked response.
            mock.Setup(m => m.Fetch<string>(It.IsAny<string>()))
                .Returns(mockResponse);

            // Create our endpoint and call ping.
            var metaEndpoint = new MetaEndpoint(mock.Object);
            var result = metaEndpoint.Ping();

            // Assert that the outcome is what was expected.
            Assert.AreEqual(result, mockResponse.Result.data);
        }
    }
}
