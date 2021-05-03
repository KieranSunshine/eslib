using System;
using System.Threading.Tasks;
using Eslib.Tests.Integration.Helpers;
using NUnit.Framework;

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
            
            MockGetRequest("/latest/alliances", stubbedData);
            
            var result = await _esi.Alliance.GetAllianceIds();

            ResponseAssert.IsSuccessful(result);
            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Length == 5);
            Assert.AreEqual(stubbedData[0], result.Data[0]);
            Assert.AreEqual(stubbedData[1], result.Data[1]);
            Assert.AreEqual(stubbedData[2], result.Data[2]);
            Assert.AreEqual(stubbedData[3], result.Data[3]);
            Assert.AreEqual(stubbedData[4], result.Data[4]);
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

            MockGetRequest("/latest/alliances/*", stubbedData);

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

        [Test]
        public async Task GetAllianceCorporationIds()
        {
            var stubbedData = new[]
            {
                98617312,
                98667839
            };
            
            MockGetRequest("/latest/alliances/*/corporations", stubbedData);

            var result = await _esi.Alliance.GetAllianceCorporationIds(1);
            
            ResponseAssert.IsSuccessful(result);
            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Length == 2);
            Assert.AreEqual(stubbedData[0], result.Data[0]);
            Assert.AreEqual(stubbedData[1], result.Data[1]);
        }

        [Test]
        public async Task GetAllianceIcon()
        {
            var stubbedData = new
            {
                px128x128 = "https://images.evetech.net/Alliance/99000006_128.png",
                px64x64 = "https://images.evetech.net/Alliance/99000006_64.png"
            };
            
            MockGetRequest("/latest/alliances/*/icons", stubbedData);

            var result = await _esi.Alliance.GetAllianceIcon(1);
            
            ResponseAssert.IsSuccessful(result);
            Assert.IsNull(result.Message);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(stubbedData.px64x64, result.Data.Url64);
            Assert.AreEqual(stubbedData.px128x128, result.Data.Url128);
        }
    }
}