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

            var response = new Response<string>(HttpStatusCode.OK, stubbedData);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(stubbedData)
            };

            // ParseResponse will call ReadAsStringAsync on the underlying HttpContent object.
            // Really this should be mocked but as we are controlling the content, I am putting trust in System.Net.Http.           
            var result = _responseFactory.Create<string>(httpResponse);

            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Data);
            Assert.IsNull(result.Error);
            Assert.AreEqual(result.StatusCode, result.StatusCode);
            Assert.AreEqual(response.Message, result.Message);
        }

        [Test]
        public void CreateResponseFromObject()
        {
            var stubbedData = new FakeType
            {
                SomeId = 42,
                SomeData = "the meaning of life"
            };

            var response = new Response<FakeType>(HttpStatusCode.OK, stubbedData);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };

            var result = _responseFactory.Create<FakeType>(httpResponse);

            if (!(result.Data is null))
            {
                Assert.AreEqual(response.Data!.SomeId, result.Data.SomeId);
                Assert.AreEqual(response.Data!.SomeData, result.Data.SomeData);
                
                Assert.IsNull(result.Error);
                Assert.IsNull(result.Message);
            }
            else
            {
                Assert.Fail();
            }
        }

        private class FakeType
        {
            public int SomeId { get; set; }

            public string SomeData { get; set; }
        }
    }
}