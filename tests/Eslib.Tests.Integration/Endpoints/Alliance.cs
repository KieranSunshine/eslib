using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using WireMock.ResponseBuilders;
using WireMock.RequestBuilders;

namespace Eslib.Tests.Integration.Endpoints
{
    [TestFixture]
    public class Alliance : EndpointBase
    {
        [Test]
        public async Task GetAllianceIds()
        {
            // Wiremock is returning no mapping found?
            // https://www.youtube.com/watch?v=YU3ohofu6UU
            var request = Request
                .Create()
                .WithPath("/latest/alliances?datasource=tranquility")
                .UsingGet();

            var response = Response
                .Create()
                .WithBody("[1, 2, 3, 4, 5]");
            
            _server
                .Given(request)
                .RespondWith(response);
            
            // TODO: If a fake URL can't be specified for WireMock then may need to create a const class to store endpoint urls.
            // Alternatively, hard code the values and allow the base url to be altered on the ESI Object
            
            // TODO: Ensure that wiremock catches the request and returns a predictable result that matches that returned by ESI.
            var result = await _esi.Alliance.GetAllianceIds();
            
            Assert.IsTrue(result.Success);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Length > 0);
        }
    }
}