using Eslib.Models.Internals;
using NUnit.Framework;
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

        [TearDown]
        public void Dispose()
        {
            _server.Stop();
        }
    }
}