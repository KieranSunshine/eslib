using System.Threading.Tasks;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class BookmarksEndpoint : EndpointBase
    {
        public BookmarksEndpoint(ApiOptions options) : base(options)
        {
            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        public BookmarksEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory)
        {
            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        public IBookmarkOwner Characters { get; }
        public IBookmarkOwner Corporations { get; }

        private class BookmarkOwner : IBookmarkOwner
        {
            private readonly string _ownerType;
            private readonly BookmarksEndpoint _parent;

            public BookmarkOwner(BookmarksEndpoint parent, string ownerType)
            {
                _parent = parent;
                _ownerType = ownerType;
            }

            public async Task<Response<Bookmark[]>> GetBookmarks(int id, int pageNumber = 1)
            {
                var request = _parent._requestFactory.Create()
                    .AddPaths(_ownerType, id.ToString(), "bookmarks")
                    .Page(pageNumber);

                var result = await _parent._dataService.Get(request);

                return _parent._responseFactory.Create<Bookmark[]>(result);
            }

            public async Task<Response<BookmarkFolder[]>> GetBookmarkFolders(int id, int pageNumber = 1)
            {
                var request = _parent._requestFactory.Create()
                    .AddPaths(_ownerType, id.ToString(), "bookmarks", "folders")
                    .Page(pageNumber);

                var result = await _parent._dataService.Get(request);

                return _parent._responseFactory.Create<BookmarkFolder[]>(result);
            }
        }
    }

    public interface IBookmarkOwner
    {
        public Task<Response<Bookmark[]>> GetBookmarks(int id, int pageNumber = 1);

        public Task<Response<BookmarkFolder[]>> GetBookmarkFolders(int id, int pageNumber = 1);
    }
}