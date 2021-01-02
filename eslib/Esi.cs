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

            meta = new Meta(_dataService);
        }

        public readonly Meta meta;
    }
}
