using System.Net.Http;
using System.Threading.Tasks;
using Eslib.Helpers.Wrappers;
using Eslib.Models.Internals;
using Eslib.Services;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Services
{
    [TestFixture]
    internal class DataServiceTests
    {
        private Mock<IHttpClientWrapper> _mockHttpClient;

        [SetUp]
        public void Init()
        {
            _mockHttpClient = new Mock<IHttpClientWrapper>();
        }
        
        [Test]
        public async Task Get()
        {
            // Create a response and set the content property.
            var request = new EsiRequest();
            var data = "ok";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(data)
            };
            var expected = Task.FromResult(response);

            // Ensure that the call to GetAsync returns our prepared HttpResponseMessage.
            _mockHttpClient
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(expected);

            // Perform our test.
            var dataService = new DataService(_mockHttpClient.Object);
            var result = await dataService.Get(request);

            // Assert the outcomes.
            Assert.AreEqual(response, result);
        }

        [Test]
        public async Task Post()
        {
            // Generate a fake object to post.
            var data = new FakeObject() {
                RandomString = "Random string",
                RandomInt = 42
            };
            var request = new EsiRequest() { Data = data };
            
            // Define the expected response from the post.
            var postResponse = "object was posted";            
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(postResponse)
            };
            var expected = Task.FromResult(response);

            // Ensure that PostAsync returns appropriately.
            _mockHttpClient
                .Setup(m => m.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .Returns(expected);

            // Conduct the test.
            var dataService = new DataService(_mockHttpClient.Object);
            var result = await dataService.Post(request);

            // Evaluate the response.
            Assert.AreEqual(response, result);
        }

        private class FakeObject 
        {
            public string RandomString { get; set; }

            public int RandomInt { get; set; }
        }
    }
}
