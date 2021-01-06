using eslib.Helpers.Wrappers;
using eslib.Models;
using eslib.Services;
using eslib.Services.Handlers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Text.Json;
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
            var mockResponseHandler = new Mock<IResponseHandler>();

            // Create a response and set the content property.
            var data = "ok";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(data)
            };
            var expectedResult = new Response<string>() { data = data };

            // Ensure that the call to GetAsync returns our prepared HttpResponseMessage.
            mockHttpClient.Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(response));

            // Ensure that ParseResponse returns our expected response.
            mockResponseHandler.Setup(m => m.Parse<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(expectedResult);

            // Perform our test.
            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object, mockResponseHandler.Object);
            var result = await dataService.Fetch<string>("");

            // Assert the outcomes.
            Assert.AreEqual(expectedResult.data, result.data);
        }        

        [Test]
        public void GenerateUrl()
        {
            var mockOptions = new Mock<IOptions<ApiOptions>>();
            var mockHttpClient = new Mock<IHttpClientWrapper>();
            var mockResponseHandler = new Mock<IResponseHandler>();

            var baseUrl = "https://esi.evetech.net";
            var endpoint = "testendpoint";
            var parameter = "somevalue";
            

            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object, mockResponseHandler.Object);

            var result = dataService.GenerateUrl(endpoint, parameter);

            Assert.AreEqual($"{baseUrl}/{endpoint}/{parameter}", result);
        }
    }
}
