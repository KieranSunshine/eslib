using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Tests.Integration.Helpers;
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
            var stubbedData = new[]
            {
                99000006,
                99000008,
                99000025,
                99000026,
                99000036
            };
            
            var request = Request
                .Create()
                .WithPath("/latest/alliances")
                .UsingGet();

            var response = Response
                .Create()
                .WithBody(JsonSerializer.Serialize(stubbedData));
            
            _server
                .Given(request)
                .RespondWith(response);
            
            var result = await _esi.Alliance.GetAllianceIds();

            ResponseAssert.IsSuccessful(result);
            
            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Length == 5);
            Assert.IsTrue(result.Data[0] == 99000006);
            Assert.IsTrue(result.Data[1] == 99000008);
            Assert.IsTrue(result.Data[2] == 99000025);
            Assert.IsTrue(result.Data[3] == 99000026);
            Assert.IsTrue(result.Data[4] == 99000036);
        }

        [Test]
        public async Task GetAlliance()
        {
            var stubbedData = new
            {
                creator_corporation_id = 665335352,
                creator_id = 549618368,
                date_founded = "2010-11-04T13:11:00Z",
                executor_corporation_id = 1983708877,
                name = "Everto Rex Regiss",
                ticker = "666"
            };
            
            var request = Request
                .Create()
                .WithPath("/latest/alliances/*")
                .UsingGet();

            var response = Response
                .Create()
                .WithBody(JsonSerializer.Serialize(stubbedData));
            
            _server
                .Given(request)
                .RespondWith(response);

            var result = await _esi.Alliance.GetAlliance(1);
            
            ResponseAssert.IsSuccessful(result);

            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(stubbedData.creator_corporation_id, result.Data.CreatorCorporationId);
            Assert.AreEqual(stubbedData.creator_id, result.Data.CreatorId);
            Assert.AreEqual(new DateTime(2010, 11, 4, 13, 11, 0), result.Data.DateFounded);
            Assert.AreEqual(stubbedData.executor_corporation_id, result.Data.ExecutorCorporationId);
            Assert.AreEqual(stubbedData.name, result.Data.Name);
            Assert.AreEqual(stubbedData.ticker, result.Data.Ticker);
        }
    }
}