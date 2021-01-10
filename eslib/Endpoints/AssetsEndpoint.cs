using eslib.Models;
using eslib.Services;
using System.Collections;

namespace eslib.Endpoints
{
    public class AssetsEndpoint
    {
        protected readonly IDataService _dataService;

        public AssetsEndpoint(IDataService dataService)
        {
            _dataService = dataService;

            Characters = new AssetOwner(this, "characters");
            Corporations = new AssetOwner(this, "corporations");
        }

        // The same endpoint methods exist for both Characters and Corporations.
        public IAssetOwner Characters;
        public IAssetOwner Corporations;


        // Define the reusable logic for the different Asset Owners.
        private class AssetOwner : IAssetOwner
        {            
            private AssetsEndpoint _parent;
            private string _ownerType;

            public AssetOwner(AssetsEndpoint parent, string ownerType)
            {
                _parent = parent;
                _ownerType = ownerType;
            }

            public Asset[] GetAssets(int id)
            {
                var url = _parent._dataService.GenerateUrl(_ownerType, id.ToString(), "assets");
                var result = _parent._dataService.Get<Asset[]>(url).Result;

                return result.data;
            }
        }
        
        public interface IAssetOwner
        {
            public Asset[] GetAssets(int id);
        }
    }
}