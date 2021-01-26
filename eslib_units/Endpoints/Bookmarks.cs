using System.Net.Http;
using System.Text.Json;
using NUnit.Framework;
using Moq;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Endpoints;
using System.Threading.Tasks;
using eslib.Services.Factories;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class BookmarksTests
    {
        private Request _stubbedRequest;

        private Mock<IRequestFactory> _mockRequestFactory;
        private Mock<IDataService> _mockDataService;
        private Mock<IResponseFactory> _mockResponseFactory;

        [SetUp]
        public void Init()
        {
            _stubbedRequest = new Request();

            _mockRequestFactory = new Mock<IRequestFactory>();
            _mockDataService = new Mock<IDataService>();
            _mockResponseFactory = new Mock<IResponseFactory>();

            _mockRequestFactory
                .Setup(m => m.Create())
                .Returns(_stubbedRequest);
        }

        [Test]
        public void GetBookmarksTests()
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
            var response = new Response<Bookmark[]>() { Data = stubbedData };

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Bookmark[]>(httpResponse))
                .Returns(response);

            var bookmarksEndpoint = new BookmarksEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var characterResult = bookmarksEndpoint.Characters.GetBookmarks(1);
            var corporationResult = bookmarksEndpoint.Corporations.GetBookmarks(2);

            Assert.AreEqual(stubbedData, characterResult.Data);
            Assert.AreEqual(stubbedData, corporationResult.Data);
        }

        [Test]
        public void GetBookmarkFolders()
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
            var response = new Response<BookmarkFolder[]>() { Data = stubbedData };

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<BookmarkFolder[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var bookmarksEndpoint = new BookmarksEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var characterResult = bookmarksEndpoint.Characters.GetBookmarkFolders(1);
            var corporationResult = bookmarksEndpoint.Corporations.GetBookmarkFolders(2);

            Assert.AreEqual(stubbedData, characterResult.Data);
            Assert.AreEqual(stubbedData, corporationResult.Data);
        }
    }
}