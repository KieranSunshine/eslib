using Eslib.Endpoints;
using Eslib.Models.Internals;
using Microsoft.Extensions.Options;

namespace Eslib
{
    public class Esi
    {
        public Esi(IOptions<ApiOptions> apiOptions)
        {
            var options = apiOptions.Value;

            Meta = new MetaEndpoint(options);
            Alliance = new AllianceEndpoint(options);
            Assets = new AssetsEndpoint(options);
            Bookmarks = new BookmarksEndpoint(options);
            Calendar = new CalendarEndpoint(options);
        }

        public MetaEndpoint Meta { get; }
        public AllianceEndpoint Alliance { get; }
        public AssetsEndpoint Assets { get; }
        public BookmarksEndpoint Bookmarks { get; }
        public CalendarEndpoint Calendar { get;  }
    }
}
