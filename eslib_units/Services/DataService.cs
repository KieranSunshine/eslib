using eslib.Helpers.Wrappers;
using eslib.Models;
using eslib.Services;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace eslib_units.Services
{
    internal class DataServiceTests
    {
        [Test]
        public async Task Fetch()
        {
            // Create a mock IOptions, IHttpClientWrapper and IDataService.
            var mockOptions = new Mock<IOptions<ApiOptions>>();
            var mockHttpClient = new Mock<IHttpClientWrapper>();
            var mockDataService = new Mock<IDataService>();

            // Create a response and set the content property.
            var message = "ok";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(message)
            };
            var expectedResult = new Response<string>() { message = message };

            // Ensure that the call to GetAsync returns our prepared HttpResponseMessage.
            mockHttpClient.Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(response));

            // Ensure that ParseResponse returns our expected response.
            mockDataService.Setup(m => m.ParseResponse<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(expectedResult);

            // Perform our test.
            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object);
            var result = await dataService.Fetch<string>("");

            // Assert the outcomes.
            Assert.AreEqual(expectedResult.message, result.message);
        }
    }
}
