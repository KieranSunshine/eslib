using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Endpoints;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class AssetsTests
    {
        private Mock<IDataService> _mockDataService;
        private Mock<IResponseFactory> _mockResponseFactory;

        [SetUp]
        public void Init()
        {
            _mockDataService = new Mock<IDataService>();
            _mockResponseFactory = new Mock<IResponseFactory>();
        }

        [Test]
        public async Task GetAssets()
        {
            var stubbedData = new [] {
                new Asset(
                    Enums.Locations.LocationFlags.Cargo.ToString(), 
                    Enums.Locations.LocationTypes.Item.ToString())
                {
                    IsBlueprintCopy = true,
                    IsSingleton = true,
                    ItemId = 1234567,                    
                    LocationId = 1234567,
                    Quantity = 42,
                    TypeId = 1234567
                },
                new Asset(
                    Enums.Locations.LocationFlags.Hangar.ToString(),
                    Enums.Locations.LocationTypes.Other.ToString())
                {
                    IsBlueprintCopy = false,
                    IsSingleton = false,
                    ItemId = 12345678,
                    LocationId = 12345678,
                    Quantity = 84,
                    TypeId = 12345678
                }
            };
            
            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<Asset[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Asset[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            // These use the same underlying code so we might as well test them all.
            var characterResult = await assetsEndpoint.Characters.GetAssets(1);
            var corporationResult = await assetsEndpoint.Corporations.GetAssets(2);

            Assert.AreEqual(response, characterResult);
            Assert.AreEqual(response, corporationResult);
        }

        [Test]
        public async Task GetAssetLocations()
        {
            var stubbedPosition = new Position(1, 2, 3);
            var stubbedIds = new List<long> {1, 2, 3, 4, 5};
            var stubbedData = new [] 
            { 
                new AssetLocation(stubbedPosition)
                {
                    ItemId = 1
                },
                new AssetLocation(stubbedPosition)
                {
                    ItemId = 2
                }
            };

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<AssetLocation[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.PostAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<AssetLocation[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            var characterResult = await assetsEndpoint.Characters.GetAssetLocations(1, stubbedIds);
            var corporationResult = await assetsEndpoint.Corporations.GetAssetLocations(2, stubbedIds);

            Assert.AreEqual(response, characterResult);
            Assert.AreEqual(response, corporationResult);
        }

        [Test]
        public void GetAssetLocationsThrowsErrorOnMinItemIds()
        {
            var stubbedIds = new List<long>();

            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Characters.GetAssetLocations(1, stubbedIds));
            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Corporations.GetAssetLocations(2, stubbedIds));
        }

        [Test]
        public void GetAssetLocationsThrowsErrorOnMaxItemIds()
        {
            // Create the list and populate it.
            var stubbedIds = new List<long>();
            for (var i = 0; i <= 1000; i++)
            {
                stubbedIds.Add(i);
            }
            
            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Characters.GetAssetLocations(1, stubbedIds));
            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Corporations.GetAssetLocations(2, stubbedIds));
        }

        [Test]
        public async Task GetAssetNames()
        {
            var testIds = new List<long> { 1, 2, 3, 4, 5 };
            var stubbedData = new []
            {
                new AssetName(1, "Apple"),
                new AssetName(2, "Banana")
            };

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<AssetName[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.PostAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<AssetName[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            var characterResult = await assetsEndpoint.Characters.GetAssetNames(1, testIds);
            var corporationResult = await assetsEndpoint.Corporations.GetAssetNames(2, testIds);

            Assert.AreEqual(response, characterResult);
            Assert.AreEqual(response, corporationResult);
        }

        [Test]
        public void GetAssetNamesThrowsErrorOnMinItemIds()
        {
            var stubbedIds = new List<long>();

            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Characters.GetAssetNames(1, stubbedIds));
            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Corporations.GetAssetNames(2, stubbedIds));
        }

        [Test]
        public void GetAssetNamesThrowsErrorOnMaxItemIds()
        {
            // Create the list and populate it.
            var stubbedIds = new List<long>();
            for (var i = 0; i <= 1000; i++)
            {
                stubbedIds.Add(i);
            }

            var assetsEndpoint = new AssetsEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Characters.GetAssetNames(1, stubbedIds));
            Assert.ThrowsAsync<ArgumentException>(
                async () => await assetsEndpoint.Corporations.GetAssetNames(2, stubbedIds));
        }
    }
}
