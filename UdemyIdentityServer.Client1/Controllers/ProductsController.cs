using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace UdemyIdentityServer.Client1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disco.IsError)
            {
                //loglama yap
            }

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

            clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            var token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (token.IsError)
            {
                //loglama yap
            }
            //https://localhost:5006

            httpClient.SetBearerToken(token.AccessToken);

            var response = await httpClient.GetAsync("https://localhost:5016/api/products/getproducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                //loglama yap
            }

            return View();
        }
    }
}