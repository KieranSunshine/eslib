using System.Threading.Tasks;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;

namespace Eslib.Endpoints
{
    public class BookmarksEndpoint : EndpointBase
    {
        #region Constructors

        public BookmarksEndpoint(ApiOptions options) : base(options)
        {
            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        public BookmarksEndpoint(
            IDataService dataService,
            IResponseFactory responseFactory)
            : base(dataService, responseFactory)
        {
            Characters = new BookmarkOwner(this, "characters");
            Corporations = new BookmarkOwner(this, "corporations");
        }

        #endregion

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

            public async Task<EsiResponse<Bookmark[]>> GetBookmarks(int id, int pageNumber = 1)
            {
                var url = new Url(_parent._baseUrl)
                    .AppendPathSegments(_ownerType, id, "bookmarks")
                    .SetQueryParam("page", pageNumber);

                var response = await _parent._dataService.GetAsync(url);

                return _parent._responseFactory.Create<Bookmark[]>(response);
            }

            public async Task<EsiResponse<BookmarkFolder[]>> GetBookmarkFolders(int id, int pageNumber = 1)
            { 
                var url = new Url(_parent._baseUrl)
                    .AppendPathSegments(_ownerType, id.ToString(), "bookmarks", "folders")
                    .SetQueryParam("page", pageNumber);

                var response = await _parent._dataService.GetAsync(url);

                return _parent._responseFactory.Create<BookmarkFolder[]>(response);
            }
        }
    }

    public interface IBookmarkOwner
    {
        public Task<EsiResponse<Bookmark[]>> GetBookmarks(int id, int pageNumber = 1);

        public Task<EsiResponse<BookmarkFolder[]>> GetBookmarkFolders(int id, int pageNumber = 1);
    }
}