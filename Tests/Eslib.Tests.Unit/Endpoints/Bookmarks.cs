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
    public class BookmarksTests
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
        public async Task GetBookmarksTests()
        {            
            var stubbedData = new [] {
                new Bookmark(
                    1,
                    new System.DateTime(2021, 1, 22),
                    1,
                    "some_label",
                    1,
                    "some_note"
                ),
                new Bookmark(
                    2,
                    new System.DateTime(2021, 1, 22),
                    2,
                    "some_other_label",
                    2,
                    "some_other_note"
                )
            };

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<Bookmark[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Bookmark[]>(httpResponse))
                .Returns(response);

            var bookmarksEndpoint = new BookmarksEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            var characterResult = await bookmarksEndpoint.Characters.GetBookmarks(1);
            var corporationResult = await bookmarksEndpoint.Corporations.GetBookmarks(2);

            Assert.AreEqual(response, characterResult);
            Assert.AreEqual(response, corporationResult);
        }

        [Test]
        public async Task GetBookmarkFolders()
        {            
            var stubbedData = new [] {
                new BookmarkFolder(1, "some_folder"),
                new BookmarkFolder(2, "some_other_folder")
            };

            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new EsiResponse<BookmarkFolder[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<BookmarkFolder[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var bookmarksEndpoint = new BookmarksEndpoint(
                _mockDataService.Object,
                _mockResponseFactory.Object);

            var characterResult = await bookmarksEndpoint.Characters.GetBookmarkFolders(1);
            var corporationResult = await bookmarksEndpoint.Corporations.GetBookmarkFolders(2);

            Assert.AreEqual(response, characterResult);
            Assert.AreEqual(response, corporationResult);
        }
    }
}