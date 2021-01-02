using eslib.Models;
using Microsoft.Extensions.Options;

namespace eslib
{
    public class Esi
    {
        private readonly IOptions<ApiOptions> _options;

        public Esi(IOptions<ApiOptions> options)
        {
            _options = options;
        }
    }
}
