using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;

namespace eslib.Endpoints
{
    public class BookmarksEndpoint : EndpointBase
    {
        public BookmarksEndpoint(ApiOptions options) : base(options)
        {
            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        public BookmarksEndpoint(IDataService dataService) : base(dataService)
        {
            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        public IBookmarkOwner Characters { get; }
        public IBookmarkOwner Corporations { get; }

        private class BookmarkOwner: IBookmarkOwner
        {
            private readonly BookmarksEndpoint _parent;
            private readonly string _ownerType;

            public BookmarkOwner(BookmarksEndpoint parent, string ownerType)
            {
                _parent = parent;
                _ownerType = ownerType;
            }

            public Response<Bookmark[]> GetBookmarks(int id)
            {
                var url = _parent._dataService.GenerateUrl(_ownerType, id.ToString(), "bookmarks");
                var result = _parent._dataService.Get<Bookmark[]>(url).Result;

                return result;
            }

            public Response<BookmarkFolder[]> GetBookmarkFolders(int id)
            {
                var url = _parent._dataService.GenerateUrl(_ownerType, id.ToString(), "bookmarks", "folders");
                var result = _parent._dataService.Get<BookmarkFolder[]>(url).Result;

                return result;
            }
        }
    }

    public interface IBookmarkOwner
    {
        public Response<Bookmark[]> GetBookmarks(int id);

        public Response<BookmarkFolder[]> GetBookmarkFolders(int id);
    }
}