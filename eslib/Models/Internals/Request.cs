using System;
using System.Collections.Generic;

namespace eslib.Models.Internals
{
    public class Request : IRequest
    {
        public Request()
        {
            Paths = new List<string>();
            Queries = new Dictionary<string, string>();
        }

        public string Url => CreateUrl();
        public List<string> Paths { get; }
        public Dictionary<string, string> Queries { get; }

        public Request AddPaths(params string[] paths)
        {
            if (paths.Length > 0)
            {
                Paths.AddRange(paths);
            }
            return this;
        }

        /// <summary>
        /// Encodes then combines paths and query parameters.
        /// </summary>
        /// <returns>A fully formed and encoded url.</returns>
        private string CreateUrl()
        {
            var encodedPaths = new List<string>();
            var encodedQueries = new List<string>();
            
            foreach (var path in Paths)
            {
                var encoded = Uri.EscapeDataString(path);
                
                encodedPaths.Add(encoded);
            }

            foreach (var (key, value) in Queries)
            {
                var encodedKey = Uri.EscapeDataString(key);
                var encodedValue = Uri.EscapeDataString(value);
                
                encodedQueries.Add($"{encodedKey}={encodedValue}");
            }

            var fullPath = $"{Constants.ApiUrl}/{string.Join("/", encodedPaths)}";
            var queryString = string.Join("&", encodedQueries);
            
            return $"{fullPath}?{queryString}";
        }
    }

    public interface IRequest
    {
        /// <summary>
        /// The fully formed and encoded url, including all paths and any query parameters.
        /// </summary>
        public string Url { get; }
        
        /// <summary>
        /// A list of paths to append to the BaseUrl.
        /// </summary>
        public List<string> Paths { get; }
        
        /// <summary>
        /// A key-value dictionary of query parameters.
        /// </summary>
        public Dictionary<string, string> Queries { get; }
        
        /// <summary>
        /// Append the provided paths to the Request.
        /// </summary>
        /// <param name="paths">The paths to add</param>
        /// <returns>The request</returns>
        public Request AddPaths(params string[] paths);
    }
}