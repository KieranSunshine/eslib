using eslib.Endpoints;
using eslib.Models.Internals;
using eslib.Services;
using Microsoft.Extensions.Options;

namespace eslib
{
    public class Esi
    {
        public Esi(IOptions<ApiOptions> options)
        {
            IDataService dataService = new DataService(options);

            Meta = new MetaEndpoint(dataService);
            Alliance = new AllianceEndpoint(dataService);
            Assets = new AssetsEndpoint(dataService);
            Bookmarks = new BookmarksEndpoint(dataService);
        }

        public readonly MetaEndpoint Meta;
        public readonly AllianceEndpoint Alliance;
        public readonly AssetsEndpoint Assets;
        public readonly BookmarksEndpoint Bookmarks;
    }
}
