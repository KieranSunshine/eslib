using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Endpoints;
using Eslib.Models;
using Eslib.Models.Internals;
using Flurl;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class BookmarksTests : EndpointTestBase<BookmarksEndpoint>
    {
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

            var characterResult = await Target.Characters.GetBookmarks(1);
            var corporationResult = await Target.Corporations.GetBookmarks(2);

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

            var characterResult = await Target.Characters.GetBookmarkFolders(1);
            var corporationResult = await Target.Corporations.GetBookmarkFolders(2);

            Assert.AreEqual(response, characterResult);
            Assert.AreEqual(response, corporationResult);
        }
    }
}