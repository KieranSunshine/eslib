
namespace Eslib.Models.Internals
{
    public class ApiOptions
    {
        public ApiOptions()
        {
            DataSource = "tranquility";
            Version = Constants.ApiVersions.Latest;
            ApiUrl = Constants.ApiUrl;
        }
        
        public string DataSource { get; }
        public string Version { get; set; }
        public string ApiUrl { get; set; }
    }
}
