using eslib.Models.Internals;
using eslib.Services.Handlers;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Text.Json;

namespace eslib_units.Services.Handlers
{
    
    public class ResponseHandlerTests
    {

        [Test]
        public void ParseResponseString()
        {
            var data = "this should be parsed correctly";
            var expectedResult = new Response<string>() { Data = data };

            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(data)
            };

            var responseHandler = new ResponseHandler();

            // ParseResponse will call ReadAsStringAsync on the underlying HttpContent object.
            // Really this should be mocked but as we are controlling the content, I am putting trust in System.Net.Http.           
            var result = responseHandler.Parse<string>(response);

            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        [Test]
        public void ParseResponseObject()
        {            
            var expectedObject = new FakeType()
            {
                SomeId = 42,
                SomeData = "the meaning of life"
            };
            var serializedObject = JsonSerializer.Serialize<FakeType>(expectedObject);

            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(serializedObject)
            };

            var responseHandler = new ResponseHandler();

            var result = responseHandler.Parse<FakeType>(response);

            Assert.AreEqual(expectedObject.SomeId, result.Data.SomeId);
            Assert.AreEqual(expectedObject.SomeData, result.Data.SomeData);
        }

        private class FakeType
        {
            public int SomeId { get; set; }

            public string SomeData { get; set; }
        }
    }
}
