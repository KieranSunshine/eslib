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
        public async Task Get()
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
            var result = await dataService.Get<string>("");

            // Assert the outcomes.
            Assert.AreEqual(expectedResult.data, result.data);
        }

        [Test]
        public async Task Post()
        {
            // Create a mock IOptions, IHttpClientWrapper and IDataService.
            var mockOptions = new Mock<IOptions<ApiOptions>>();
            var mockHttpClient = new Mock<IHttpClientWrapper>();
            var mockResponseHandler = new Mock<IResponseHandler>();

            // Generate a fake object to post.
            var data = new FakeObject() {
                RandomString = "Random string",
                RandomInt = 42
            };

            // Define the expected response from the post.
            var postResponse = "object was posted";            
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(postResponse)
            };

            // Generate the expected end result.
            var expectedResult = new Response<string>() { data = postResponse };

            // Ensure that PostAsync returns appropriately.
            mockHttpClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(response));
            
            // Ensure that Parse returns appropriately.
            mockResponseHandler.Setup(m => m.Parse<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(expectedResult);

            // Conduct the test.
            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object, mockResponseHandler.Object);
            var result = await dataService.Post<string>("", data);

            // Evaluate the response.
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

        private class FakeObject 
        {
            public string RandomString { get; set; }

            public int RandomInt { get; set; }
        }
    }
}
