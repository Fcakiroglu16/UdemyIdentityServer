using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyIdentityServer.Client1.Models;

namespace UdemyIdentityServer.Client1.Services
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClient();

        Task<List<string>> SaveUserViewModel(UserSaveViewModel userSaveViewModel);
    }
}