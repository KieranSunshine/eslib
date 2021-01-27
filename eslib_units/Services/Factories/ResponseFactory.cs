using System.Net;
using System.Net.Http;
using System.Text.Json;
using eslib.Models.Internals;
using eslib.Services.Factories;
using NUnit.Framework;

namespace eslib_units.Services.Factories
{
    [TestFixture]
    public class ResponseFactoryTests
    {
        [SetUp]
        public void Init()
        {
            _responseFactory = new ResponseFactory();
        }

        private IResponseFactory _responseFactory;

        [Test]
        public void CreateResponseFromString()
        {
            var stubbedData = "this should be parsed correctly";

            var response = new Response<string> {Data = stubbedData};
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(stubbedData)
            };

            // ParseResponse will call ReadAsStringAsync on the underlying HttpContent object.
            // Really this should be mocked but as we are controlling the content, I am putting trust in System.Net.Http.           
            var result = _responseFactory.Create<string>(httpResponse);

            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);

            Assert.AreEqual(response.Data, result.Data);
            Assert.AreEqual(response.Error, result.Error);
        }

        [Test]
        public void CreateResponseFromObject()
        {
            var stubbedData = new FakeType
            {
                SomeId = 42,
                SomeData = "the meaning of life"
            };

            var response = new Response<FakeType> {Data = stubbedData};
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };

            var result = _responseFactory.Create<FakeType>(httpResponse);

            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);

            Assert.AreEqual(response.Data.SomeId, result.Data.SomeId);
            Assert.AreEqual(response.Data.SomeData, result.Data.SomeData);
            Assert.AreEqual(response.Error, result.Error);
        }

        private class FakeType
        {
            public int SomeId { get; set; }

            public string SomeData { get; set; }
        }
    }
}