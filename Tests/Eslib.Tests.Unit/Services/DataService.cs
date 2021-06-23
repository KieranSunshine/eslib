using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Eslib.Helpers.Wrappers;
using Eslib.Models.Internals;
using Eslib.Services;
using Flurl;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Services
{
    [TestFixture]
    internal class DataServiceTests
    {
        private ApiOptions _options;
        private Mock<IHttpClientWrapper> _mockHttpClient;

        [SetUp]
        public void Init()
        {
            _options = new ApiOptions();
            _mockHttpClient = new Mock<IHttpClientWrapper>();
        }
        
        [Test]
        public async Task GetAsync()
        {
            var stubbedUrl = new Url();
            var stubbedData = "ok";
            var stubbedResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(stubbedData)
            };
            
            _mockHttpClient
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(stubbedResponseMessage));
        
            // Perform our test.
            var dataService = new DataService(_options, _mockHttpClient.Object);
            var result = await dataService.GetAsync(stubbedUrl);
        
            // Assert the outcomes.
            Assert.AreEqual(stubbedResponseMessage, result);
            Assert.AreEqual(stubbedResponseMessage.Content, result.Content);
        }

        [Test]
        public async Task PostAsync()
        {
            // Generate a fake object to post.
            var stubbedUrl = new Url();
            var stubbedData = new FakeObject() {
                RandomString = "Random string",
                RandomInt = 42
            };
            var stubbedPostResponse = "object was posted";            
            var stubbedResponse = new HttpResponseMessage()
            {
                Content = new StringContent(stubbedPostResponse)
            };
            
            _mockHttpClient
                .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>()))
                .Returns(Task.FromResult(stubbedResponse));

            // Conduct the test.
            var dataService = new DataService(_options, _mockHttpClient.Object);
            var result = await dataService.PostAsync(stubbedUrl, stubbedData);

            // Evaluate the response.
            Assert.AreEqual(stubbedResponse, result);
            Assert.AreEqual(stubbedResponse.Content, result.Content);
        }

        private class FakeObject 
        {
            public string RandomString { get; set; }

            public int RandomInt { get; set; }
        }
    }
}
