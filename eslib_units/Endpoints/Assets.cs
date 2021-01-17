using NUnit.Framework;
using Moq;
using eslib.Models;
using eslib.Services;
using eslib.Endpoints;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        [Test]
        public void GetAssetLocations()
        {
            var mock = new Mock<IDataService>();

            var testIds = new List<long> { 1, 2, 3, 4, 5 };
            var expectedObject = new AssetLocation[] 
            { 
                new AssetLocation()
                {
                    ItemId = 1,
                    Postition = new Position() 
                    {
                        X = 1,
                        Y = 2,
                        Z = 3
                    }
                },
                new AssetLocation()
                {
                    ItemId = 2,
                    Postition = new Position()
                    {
                        X = 4,
                        Y = 5,
                        Z = 6
                    }
                }
            };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("something");

            mock.Setup(m => m.Post<AssetLocation[]>(It.IsAny<string>(), It.IsAny<List<long>>()))
                .Returns(Task.FromResult(new Response<AssetLocation[]>() { data = expectedObject }));

            var assetsEndpoint = new AssetsEndpoint(mock.Object);

            var characterResult = assetsEndpoint.Characters.GetAssetLocations(1, testIds);
            var corporationResult = assetsEndpoint.Corporations.GetAssetLocations(2, testIds);

            Assert.AreEqual(expectedObject, characterResult);
            Assert.AreEqual(expectedObject, corporationResult);
        }

        [Test]
        public void GetAssetLocationsThrowsErrorOnMinItemIds()
        {
            var mock = new Mock<IDataService>();

            var testIds = new List<long>();

            var assetsEndpoint = new AssetsEndpoint(mock.Object);

            Assert.Throws<ArgumentException>(() => assetsEndpoint.Characters.GetAssetLocations(1, testIds));
            Assert.Throws<ArgumentException>(() => assetsEndpoint.Corporations.GetAssetLocations(2, testIds));
        }

        [Test]
        public void GetAssetLocationsThrowsErrorOnMaxItemIds()
        {
            var mock = new Mock<IDataService>();

            // Create the list and populate it.
            var testIds = new List<long>();
            for (var i = 0; i <= 1000; i++)
            {
                testIds.Add(i);
            }
            
            var assetsEndpoint = new AssetsEndpoint(mock.Object);

            Assert.Throws<ArgumentException>(() => assetsEndpoint.Characters.GetAssetLocations(1, testIds));
            Assert.Throws<ArgumentException>(() => assetsEndpoint.Corporations.GetAssetLocations(2, testIds));

        }

        [Test]
        public void GetAssetNames()
        {
            var mock = new Mock<IDataService>();
            var testIds = new List<long> { 1, 2, 3, 4, 5 };
            var expectedObject = new AssetName[]
            {
                new AssetName()
                {
                    ItemId = 1,
                    Name = "Apple"
                },
                new AssetName()
                {
                    ItemId = 2,
                    Name = "Banana"
                }
            };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("something");

            mock.Setup(m => m.Post<AssetName[]>(It.IsAny<string>(), It.IsAny<List<long>>()))
                .Returns(Task.FromResult(new Response<AssetName[]>() { data = expectedObject }));

            var assetsEndpoint = new AssetsEndpoint(mock.Object);

            var characterResult = assetsEndpoint.Characters.GetAssetNames(1, testIds);
            var corporationResult = assetsEndpoint.Corporations.GetAssetNames(2, testIds);

            Assert.AreEqual(expectedObject, characterResult);
            Assert.AreEqual(expectedObject, corporationResult);
        }

        [Test]
        public void GetAssetNamesThrowsErrorOnMinItemIds()
        {
            var mock = new Mock<IDataService>();

            var testIds = new List<long>();

            var assetsEndpoint = new AssetsEndpoint(mock.Object);

            Assert.Throws<ArgumentException>(() => assetsEndpoint.Characters.GetAssetNames(1, testIds));
            Assert.Throws<ArgumentException>(() => assetsEndpoint.Corporations.GetAssetNames(2, testIds));
        }
    }
}
