using eslib.Models;
using eslib.Services;

namespace eslib.Endpoints
{
    public class BookmarksEndpoint
    {
        protected readonly IDataService _dataService;

        public BookmarksEndpoint(IDataService dataService)
        {
            _dataService = dataService;

            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        public IBookmarkOwner Characters;
        public IBookmarkOwner Corporations;

        private class BookmarkOwner: IBookmarkOwner
        {
            private BookmarksEndpoint _parent;
            private string _ownerType;

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