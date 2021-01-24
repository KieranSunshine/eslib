using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using System;
using System.Collections.Generic;

namespace eslib.Endpoints
{
    public class AssetsEndpoint : EndpointBase
    {
        public AssetsEndpoint(ApiOptions options) : base(options)
        {
            Characters = new AssetOwner(this, "characters");
            Corporations = new AssetOwner(this, "corporations");
        }

        public AssetsEndpoint(IDataService dataService) : base(dataService)
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
            private readonly AssetsEndpoint _parent;
            private readonly string _ownerType;

            public AssetOwner(AssetsEndpoint parent, string ownerType)
            {
                _parent = parent;
                _ownerType = ownerType;
            }

            public Response<Asset[]> GetAssets(int id)
            {
                var url = _parent._dataService.GenerateUrl(_ownerType, id.ToString(), "assets");
                var result = _parent._dataService.Get<Asset[]>(url).Result;

                return result;
            }

            public Response<AssetLocation[]> GetAssetLocations(int id, List<long> itemIds)
            {                
                if (itemIds.Count == 0 || itemIds.Count > 1000)
                {
                    throw new ArgumentException("The parameter itemIds expects an array with at least 1 element and a maximum of 1000.");
                }

                var url = _parent._dataService.GenerateUrl(_ownerType, id.ToString(), "assets", "locations");                
                var result = _parent._dataService.Post<AssetLocation[]>(url, itemIds).Result;

                return result;
            }

            public Response<AssetName[]> GetAssetNames(int id, List<long> itemIds)
            {
                if (itemIds.Count == 0 || itemIds.Count > 1000)
                {
                    throw new ArgumentException("The parameter itemIds expects an array with at least 1 element and a maximum of 1000.");
                }

                var url = _parent._dataService.GenerateUrl(_ownerType, id.ToString(), "assets", "names");
                var result = _parent._dataService.Post<AssetName[]>(url, itemIds).Result;

                return result;
            }
        }
        
        public interface IAssetOwner
        {
            public Response<Asset[]> GetAssets(int id);

            public Response<AssetLocation[]> GetAssetLocations(int id, List<long> itemIds);

            public Response<AssetName[]> GetAssetNames(int id, List<long> itemIds);
        }
    }
}