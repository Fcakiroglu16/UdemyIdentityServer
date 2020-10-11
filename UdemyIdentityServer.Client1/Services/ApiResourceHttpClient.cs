using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdemyIdentityServer.Client1.Models;

namespace UdemyIdentityServer.Client1.Services
{
    public class ApiResourceHttpClient : IApiResourceHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        private HttpClient _client;

        public ApiResourceHttpClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _client = new HttpClient();
        }

        public async Task<HttpClient> GetHttpClient()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            _client.SetBearerToken(accessToken);

            return _client;
        }

        public async Task<List<string>> SaveUserViewModel(UserSaveViewModel userSaveViewModel)
        {
            var disco = await _client.GetDiscoveryDocumentAsync(_configuration["AuthServerUrl"]);

            if (disco.IsError)
            {
                //loglama yap
            }

            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

            clientCredentialsTokenRequest.ClientId = _configuration["ClientResourceOwner:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["ClientResourceOwner:ClientSecret"];
            clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            var token = await _client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (token.IsError)
            {
                //loglama yap
            }

            var stringConent = new StringContent(JsonConvert.SerializeObject(userSaveViewModel), Encoding.UTF8, "application/json");

            _client.SetBearerToken(token.AccessToken);

            var response = await _client.PostAsync("https://localhost:5001/api/user/signup", stringConent);

            if (!response.IsSuccessStatusCode)
            {
                var errorList = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

                return errorList;
            }

            return null;
        }
    }
}