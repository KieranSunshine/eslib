using NUnit.Framework;
using Moq;
using eslib.Models;
using eslib.Services;
using eslib.Endpoints;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    [TestFixture]
    public class BookmarksTests
    {
        private Mock<IDataService> mockDataService { get; set; }

        [SetUp]
        public void Init()
        {
            mockDataService = new Mock<IDataService>();
            
            mockDataService.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("something");
        }

        [Test]
        public void GetBookmarksTests()
        {            
            var expectedObject = new Bookmark[] {
                new Bookmark(1, new System.DateTime(2021, 1, 22), 1, "some_label", 1, "some_note"),
                new Bookmark(2, new System.DateTime(2021, 1, 22), 2, "some_other_label", 2, "some_other_note")
            };
            var mockResponse = new Response<Bookmark[]>() { Data = expectedObject };

            mockDataService.Setup(m => m.Get<Bookmark[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var bookmarksEndpoint = new BookmarksEndpoint(mockDataService.Object);

            var characterResult = bookmarksEndpoint.Characters.GetBookmarks(1);
            var corporationResult = bookmarksEndpoint.Corporations.GetBookmarks(2);

            Assert.AreEqual(expectedObject, characterResult.Data);
            Assert.AreEqual(expectedObject, corporationResult.Data);
        }

        [Test]
        public void GetBookmarkFolders()
        {            
            var expectedObject = new BookmarkFolder[] {
                new BookmarkFolder(1, "some_folder"),
                new BookmarkFolder(2, "some_other_folder")
            };
            var mockResponse = new Response<BookmarkFolder[]>() { Data = expectedObject };

            mockDataService.Setup(m => m.Get<BookmarkFolder[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var bookmarksEndpoint = new BookmarksEndpoint(mockDataService.Object);

            var characterResult = bookmarksEndpoint.Characters.GetBookmarkFolders(1);
            var corporationResult = bookmarksEndpoint.Corporations.GetBookmarkFolders(2);

            Assert.AreEqual(expectedObject, characterResult.Data);
            Assert.AreEqual(expectedObject, corporationResult.Data);
        }
    }
}