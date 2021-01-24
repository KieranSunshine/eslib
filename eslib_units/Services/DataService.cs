using System.Net.Http;
using System.Threading.Tasks;
using eslib.Helpers.Wrappers;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace eslib_units.Services
{
    [TestFixture]
    internal class DataServiceTests
    {
        private Mock<ApiOptions> _mockOptions;
        private Mock<IHttpClientWrapper> _mockHttpClient;
        private Mock<IResponseFactory> _mockResponseHandler;

        [SetUp]
        public void Init()
        {
            _mockOptions = new Mock<ApiOptions>();
            _mockHttpClient = new Mock<IHttpClientWrapper>();
            _mockResponseHandler = new Mock<IResponseFactory>();
        }
        
        [Test]
        public async Task Get()
        {
            // Create a response and set the content property.
            var data = "ok";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(data)
            };
            var expectedResult = new Response<string>() { Data = data };

            // Ensure that the call to GetAsync returns our prepared HttpResponseMessage.
            _mockHttpClient.Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(response));

            // Ensure that ParseResponse returns our expected response.
            _mockResponseHandler.Setup(m => m.CreateResponse<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(expectedResult);

            // Perform our test.
            var dataService = new DataService(_mockOptions.Object, _mockHttpClient.Object, _mockResponseHandler.Object);
            var result = await dataService.Get<string>("");

            // Assert the outcomes.
            Assert.AreEqual(expectedResult.Data, result.Data);
        }

        [Test]
        public async Task Post()
        {
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
            var expectedResult = new Response<string>() { Data = postResponse };

            // Ensure that PostAsync returns appropriately.
            _mockHttpClient.Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(response));
            
            // Ensure that Parse returns appropriately.
            _mockResponseHandler.Setup(m => m.CreateResponse<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(expectedResult);

            // Conduct the test.
            var dataService = new DataService(_mockOptions.Object, _mockHttpClient.Object, _mockResponseHandler.Object);
            var result = await dataService.Post<string>("", data);

            // Evaluate the response.
            Assert.AreEqual(expectedResult.Data, result.Data);
        }

        [Test]
        public void GenerateUrl()
        {
            var baseUrl = "https://esi.evetech.net";
            var endpoint = "testendpoint";
            var parameter = "somevalue";

            var dataService = new DataService(_mockOptions.Object, _mockHttpClient.Object, _mockResponseHandler.Object);

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
