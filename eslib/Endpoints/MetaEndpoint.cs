﻿using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class MetaEndpoint : EndpointBase
    {
        public MetaEndpoint(ApiOptions options) : base(options) { }

        public MetaEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory) { }

        public Response<string> Ping()
        {
            var request = _requestFactory.Create()
                .AddPaths("ping");

            var result = _dataService.Get<string>(request).Result;

            return _responseFactory.Create<string>(result);
        }
    }
}
