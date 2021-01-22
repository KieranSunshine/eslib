using NUnit.Framework;
using Moq;
using eslib.Models;
using eslib.Services;
using eslib.Endpoints;
using System.Threading.Tasks;

namespace eslib_units.Endpoints
{
    public class BookmarksTests
    {
        [Test]
        public void GetBookmarksTests()
        {
            var mock = new Mock<IDataService>();
            var expectedObject = new Bookmark[] {
                new Bookmark(1, new System.DateTime(2021, 1, 22), 1, "some_label", 1, "some_note"),
                new Bookmark(2, new System.DateTime(2021, 1, 22), 2, "some_other_label", 2, "some_other_note")
            };
            var mockResponse = new Response<Bookmark[]>() { Data = expectedObject };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("something");

            mock.Setup(m => m.Get<Bookmark[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var bookmarksEndpoint = new BookmarksEndpoint(mock.Object);

            var characterResult = bookmarksEndpoint.Characters.GetBookmarks(1);
            var corporationResult = bookmarksEndpoint.Corporations.GetBookmarks(2);

            Assert.AreEqual(expectedObject, characterResult.Data);
            Assert.AreEqual(expectedObject, corporationResult.Data);
        }

        [Test]
        public void GetBookmarkFolders()
        {
            var mock = new Mock<IDataService>();
            var expectedObject = new BookmarkFolder[] {
                new BookmarkFolder(1, "some_folder"),
                new BookmarkFolder(2, "some_other_folder")
            };
            var mockResponse = new Response<BookmarkFolder[]>() { Data = expectedObject };

            mock.Setup(m => m.GenerateUrl(It.IsAny<string>()))
                .Returns("something");

            mock.Setup(m => m.Get<BookmarkFolder[]>(It.IsAny<string>()))
                .Returns(Task.FromResult(mockResponse));

            var bookmarksEndpoint = new BookmarksEndpoint(mock.Object);

            var characterResult = bookmarksEndpoint.Characters.GetBookmarkFolders(1);
            var corporationResult = bookmarksEndpoint.Corporations.GetBookmarkFolders(2);

            Assert.AreEqual(expectedObject, characterResult.Data);
            Assert.AreEqual(expectedObject, corporationResult.Data);
        }
    }
}