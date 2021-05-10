using Eslib.Endpoints;
using Eslib.Models.Internals;
using Microsoft.Extensions.Options;

namespace Eslib
{
    #pragma warning disable CS8618
    public class Esi
    {
        private EsiTokens _tokens = new();
        
        public Esi(IOptions<ApiOptions> apiOptions)
        {
            Init(apiOptions.Value);
        }

        public Esi(ApiOptions apiOptions)
        {
            Init(apiOptions);
        }
        
        public EsiTokens Tokens => _tokens;

        #region Endpoints

        public MetaEndpoint Meta { get; private set; }
        public AllianceEndpoint Alliance { get; private set; }
        public AssetsEndpoint Assets { get; private set; }
        public BookmarksEndpoint Bookmarks { get; private set; }
        public CalendarEndpoint Calendar { get; private set; }

        #endregion

        private void Init(ApiOptions apiOptions)
        {
            Meta = new MetaEndpoint(apiOptions);
            Alliance = new AllianceEndpoint(apiOptions);
            Assets = new AssetsEndpoint(apiOptions);
            Bookmarks = new BookmarksEndpoint(apiOptions);
            Calendar = new CalendarEndpoint(apiOptions);
        }
    }
    #pragma warning restore CS8618
}
