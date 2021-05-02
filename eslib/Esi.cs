using Eslib.Endpoints;
using Eslib.Models.Internals;
using Microsoft.Extensions.Options;

namespace Eslib
{
    public class Esi
    {
        public Esi(IOptions<ApiOptions> apiOptions)
        {
            Init(apiOptions.Value);
        }

        public Esi(ApiOptions apiOptions)
        {
            Init(apiOptions);
        }

        public MetaEndpoint Meta { get; private set; }
        public AllianceEndpoint Alliance { get; private set; }
        public AssetsEndpoint Assets { get; private set; }
        public BookmarksEndpoint Bookmarks { get; private set; }
        public CalendarEndpoint Calendar { get; private set; }

        private void Init(ApiOptions apiOptions)
        {
            Meta = new MetaEndpoint(apiOptions);
            Alliance = new AllianceEndpoint(apiOptions);
            Assets = new AssetsEndpoint(apiOptions);
            Bookmarks = new BookmarksEndpoint(apiOptions);
            Calendar = new CalendarEndpoint(apiOptions);
        }
    }
}
