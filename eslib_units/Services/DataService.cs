using eslib.Helpers.Wrappers;
using eslib.Models;
using eslib.Services;
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
            var mockDataService = new Mock<IDataService>();

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
            mockDataService.Setup(m => m.ParseResponse<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(expectedResult);

            // Perform our test.
            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object);
            var result = await dataService.Fetch<string>("");

            // Assert the outcomes.
            Assert.AreEqual(expectedResult.data, result.data);
        }

        [Test]
        public void ParseResponseString()
        {            
            var mockOptions = new Mock<IOptions<ApiOptions>>();
            var mockHttpClient = new Mock<IHttpClientWrapper>();

            var data = "this should be parsed correctly";
            var expectedResult = new Response<string>() { data = data };                       

            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(data)
            };           

            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object);

            // ParseResponse will call ReadAsStringAsync on the underlying HttpContent object.
            // Really this should be mocked but as we are controlling the content, I am putting trust in System.Net.Http.           
            var result = dataService.ParseResponse<string>(response);

            Assert.AreEqual(result.data, expectedResult.data);
        }

        [Test]
        public void ParseResponseObject()
        {
            var mockOptions = new Mock<IOptions<ApiOptions>>();
            var mockHttpClient = new Mock<IHttpClientWrapper>();

            var expectedObject = new FakeType() {
                SomeId = 42,
                SomeData = "the meaning of life"
            };
            var serializedObject = JsonSerializer.Serialize<FakeType>(expectedObject);

            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(serializedObject)
            };

            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object);

            var result = dataService.ParseResponse<FakeType>(response);

            Assert.AreEqual(expectedObject.SomeId, result.data.SomeId);
            Assert.AreEqual(expectedObject.SomeData, result.data.SomeData);
        }

        [Test]
        public void GenerateUrl()
        {
            var mockOptions = new Mock<IOptions<ApiOptions>>();
            var mockHttpClient = new Mock<IHttpClientWrapper>();

            var baseUrl = "https://esi.evetech.net";
            var endpoint = "testendpoint";
            var parameter = "somevalue";
            

            var dataService = new DataService(mockOptions.Object, mockHttpClient.Object);

            var result = dataService.GenerateUrl(endpoint, parameter);

            Assert.AreEqual($"{baseUrl}/{endpoint}/{parameter}", result);
        }

        private class FakeType
        {
            public int SomeId { get; set; }

            public string SomeData { get; set; }
        }
    }
}
