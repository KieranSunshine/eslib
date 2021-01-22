using eslib.Endpoints;
using eslib.Models;
using eslib.Services;
using Microsoft.Extensions.Options;

namespace eslib
{
    public class Esi
    {
        private readonly IOptions<ApiOptions> _options;
        private readonly IDataService _dataService;

        public Esi(IOptions<ApiOptions> options)
        {
            _options = options;
            _dataService = new DataService(_options);

            Meta = new MetaEndpoint(_dataService);
            Alliance = new AllianceEndpoint(_dataService);
            Assets = new AssetsEndpoint(_dataService);
            Bookmarks = new BookmarksEndpoint(_dataService);
        }

        public readonly MetaEndpoint Meta;
        public readonly AllianceEndpoint Alliance;
        public readonly AssetsEndpoint Assets;
        public readonly BookmarksEndpoint Bookmarks;
    }
}
