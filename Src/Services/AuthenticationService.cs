using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Helpers.Wrappers;
using Eslib.Models.Internals;
using Flurl;
using JWT;
using JWT.Algorithms;
using JWT.Builder;

namespace Eslib.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Properties

        public string ClientId { get; set; } = string.Empty;
        public string SecretKey { private get; set; } = string.Empty;
        public EsiTokens Tokens { get; set; } = new();

        #endregion

        #region Constructors

        public AuthenticationService()
        {
            _httpClient = new HttpClientWrapper();
        }

        public AuthenticationService(EsiTokens tokens)
        {
            Tokens = tokens;
            _httpClient = new HttpClientWrapper();
        }

        public AuthenticationService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Fields

        private readonly IHttpClientWrapper _httpClient;
        private const string SSOUrl = "https://login.eveonline.com";
        private readonly string OAuthUrl = $"{SSOUrl}/v2/oauth/";

        #endregion
        
        public async Task<bool> RequestTokensAsync()
        {
            var url = new Url(OAuthUrl)
                .AppendPathSegments("token");

            var request = new HttpRequestMessage
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Basic", GetEncodedCredentials()),
                    Host = "login.eveonline.com"
                },
                Method = HttpMethod.Post,
                RequestUri = url.ToUri(),
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            var result = await _httpClient.SendAsync(request);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                return false;
            
            var response = JsonSerializer.Deserialize<AuthenticationResponse>(content);
            if (response is null)
                return false;
            
            var accessToken = DecodeAccessToken(response.AccessToken);
            Tokens = new EsiTokens
            {
                Access = accessToken,
                Refresh = response.RefreshToken
            };
            
            return true;
        }

        private async Task RetrieveTokenKeys()
        {
            var url = new Url(SSOUrl)
                .AppendPathSegments("oauth", "jwks");

            var request = new HttpRequestMessage
            {
                RequestUri = url.ToUri(),
                Method = HttpMethod.Get
            };

            var result = await _httpClient.SendAsync(request);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            var keys = await JsonSerializer.DeserializeAsync<>(content);
        }

        private string GetEncodedCredentials()
        {
            if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(SecretKey))
                throw new InvalidOperationException("No ClientId or Secret Key provided");
            
            var credentials = $"{ClientId}:{SecretKey}";
            var bytes = Encoding.UTF8.GetBytes(credentials);

            return Convert.ToBase64String(bytes);
        }

        private AccessToken DecodeAccessToken(string accessToken)
        {
            throw new NotImplementedException();
            // var token = JwtBuilder.Create()
            //     .WithAlgorithm(new RS256Algorithm())
            //     .MustVerifySignature()
            //     .Decode<AccessToken>(accessToken);

        }
    }

    public interface IAuthenticationService
    {
        public Task<bool> RequestTokensAsync();
    }
}