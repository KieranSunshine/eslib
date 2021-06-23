using System.Text.Json;
using Eslib.Models.Internals;
using NUnit.Framework;
using WireMock.ResponseBuilders;
using WireMock.RequestBuilders;
using WireMock.Server;

namespace Eslib.Tests.Integration.Endpoints
{
    public class EndpointBase
    {
        protected WireMockServer _server;
        protected Esi _esi;
        
        [SetUp]
        public void Init()
        {
            _server = WireMockServer.Start();
            
            var options = new ApiOptions
            {
                ApiUrl = _server.Urls[0]
            };
            _esi = new Esi(options);
        }

        protected void MockGetRequest(string path, object stubbedData)
        {
            var request = Request
                .Create()
                .WithPath(path)
                .UsingGet();

            var response = Response
                .Create()
                .WithBody(JsonSerializer.Serialize(stubbedData));
            
            _server
                .Given(request)
                .RespondWith(response);
        }

        [TearDown]
        public void Dispose()
        {
            _server.Stop();
        }
    }
}