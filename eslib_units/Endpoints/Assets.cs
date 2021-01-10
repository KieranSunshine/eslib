using NUnit.Framework;
using Moq;
using eslib.Models;
using eslib.Services;
using eslib.Endpoints;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    public class AssetsTests
    {
        [Test]
        public void GetAssets()
        {
            var mock = new Mock<IDataService>();
            var expectedObject = new Asset[] {
                new Asset()
                {
                    IsBlueprintCopy = true,
                    IsSingleton = true,
                    ItemId = 1234567,
                    LocationFlag = "some_string",
                    LocationId = 1234567,
                    LocationType = "some_string",
                    Quantity = 42,
                    TypeId = 1234567
                },
                new Asset()
                {
                    IsBlueprintCopy = false,
                    IsSingleton = false,
                    ItemId = 12345678,
                    LocationFlag = "some_other_string",
                    LocationId = 12345678,
                    LocationType = "some_other_string",
                    Quantity = 84,
                    TypeId = 12345678
                }
            };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("something");

            mock.Setup(m => m.Get<Asset[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(new Response<Asset[]>() { data = expectedObject }));

            var assetsEndpoint = new AssetsEndpoint(mock.Object);

            // These use the same underlying code so we might as well test them all.
            var characterResult = assetsEndpoint.Characters.GetAssets(1);
            var corporationResult = assetsEndpoint.Corporations.GetAssets(2);

            Assert.AreEqual(expectedObject, characterResult);
            Assert.AreEqual(expectedObject, corporationResult);
        }
    }
}
