using System;
using System.Collections.Generic;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class AssetsEndpoint : EndpointBase
    {
        public AssetsEndpoint(ApiOptions options) : base(options)
        {
            Characters = new AssetOwner(this, "characters");
            Corporations = new AssetOwner(this, "corporations");
        }

        public AssetsEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory)
        {
            Characters = new AssetOwner(this, "characters");
            Corporations = new AssetOwner(this, "corporations");
        }

        // The same endpoint methods exist for both Characters and Corporations.
        public IAssetOwner Characters { get; }
        public IAssetOwner Corporations { get; }

        // Define the reusable logic for the different Asset Owners.
        private class AssetOwner : IAssetOwner
        {
            private readonly string _ownerType;
            private readonly AssetsEndpoint _parent;

            public AssetOwner(AssetsEndpoint parent, string ownerType)
            {
                _parent = parent;
                _ownerType = ownerType;
            }

            public Response<Asset[]> GetAssets(int id, int pageNumber = 1)
            {
                var request = _parent._requestFactory.Create()
                    .AddPaths(_ownerType, id.ToString(), "assets")
                    .Page(pageNumber);

                var result = _parent._dataService.Get(request).Result;

                return _parent._responseFactory.Create<Asset[]>(result);
            }

            public Response<AssetLocation[]> GetAssetLocations(int id, List<long> itemIds)
            {
                var request = _parent._requestFactory.Create()
                    .AddPaths(_ownerType, id.ToString(), "assets", "locations");

                if (itemIds.Count == 0 || itemIds.Count > 1000)
                    throw new ArgumentException(
                        "The parameter itemIds expects an array with at least 1 element and a maximum of 1000.");
                request.Data = itemIds;

                var result = _parent._dataService.Post(request).Result;

                return _parent._responseFactory.Create<AssetLocation[]>(result);
            }

            public Response<AssetName[]> GetAssetNames(int id, List<long> itemIds)
            {
                var request = _parent._requestFactory.Create()
                    .AddPaths(_ownerType, id.ToString(), "assets", "names");

                if (itemIds.Count == 0 || itemIds.Count > 1000)
                    throw new ArgumentException(
                        "The parameter itemIds expects an array with at least 1 element and a maximum of 1000.");
                request.Data = itemIds;

                var result = _parent._dataService.Post(request).Result;

                return _parent._responseFactory.Create<AssetName[]>(result);
            }
        }

        public interface IAssetOwner
        {
            public Response<Asset[]> GetAssets(int id, int pageNumber = 1);

            public Response<AssetLocation[]> GetAssetLocations(int id, List<long> itemIds);

            public Response<AssetName[]> GetAssetNames(int id, List<long> itemIds);
        }
    }
}