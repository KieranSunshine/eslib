using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;

namespace Eslib.Endpoints
{
    public class AssetsEndpoint : EndpointBase
    {
        #region Constructors

        public AssetsEndpoint(IDataService dataService, IAuthenticationService authenticationService) 
            : base(dataService, authenticationService)
        {
            Characters = new AssetOwner(this, "characters");
            Corporations = new AssetOwner(this, "corporations");
        }

        public AssetsEndpoint(
            IDataService dataService,
            IAuthenticationService authenticationService,
            IResponseFactory responseFactory)
            : base(dataService, authenticationService, responseFactory)
        {
            Characters = new AssetOwner(this, "characters");
            Corporations = new AssetOwner(this, "corporations");
        }

        #endregion

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

            public async Task<EsiResponse<Asset[]>> GetAssets(int id, int pageNumber = 1)
            {
                var url = new Url()
                    .AppendPathSegments(_ownerType, id, "assets")
                    .SetQueryParam("page", pageNumber);
                
                var response = await _parent._dataService.GetAsync(url);

                return _parent._responseFactory.Create<Asset[]>(response);
            }

            public async Task<EsiResponse<AssetLocation[]>> GetAssetLocations(int id, List<long> itemIds)
            {
                var url = new Url()
                    .AppendPathSegments(_ownerType, id, "assets", "locations");

                if (itemIds.Count == 0 || itemIds.Count > 1000)
                    throw new ArgumentException(
                        "The parameter itemIds expects an array with at least 1 element and a maximum of 1000.");

                var response = await _parent._dataService.PostAsync(url, itemIds);

                return _parent._responseFactory.Create<AssetLocation[]>(response);
            }

            public async Task<EsiResponse<AssetName[]>> GetAssetNames(int id, List<long> itemIds)
            {
                var url = new Url()
                    .AppendPathSegments(_ownerType, id, "assets", "names");

                if (itemIds.Count is 0 or > 1000)
                    throw new ArgumentException(
                        "The parameter itemIds expects an array with at least 1 element and a maximum of 1000.");

                var response = await _parent._dataService.PostAsync(url, itemIds);

                return _parent._responseFactory.Create<AssetName[]>(response);
            }
        }

        public interface IAssetOwner
        {
            public Task<EsiResponse<Asset[]>> GetAssets(int id, int pageNumber = 1);

            public Task<EsiResponse<AssetLocation[]>> GetAssetLocations(int id, List<long> itemIds);

            public Task<EsiResponse<AssetName[]>> GetAssetNames(int id, List<long> itemIds);
        }
    }
}