using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyIdentityServer.AuthServer.Models;

namespace UdemyIdentityServer.AuthServer.Repository
{
    internal interface ICustomUserRepository
    {
        Task<bool> Validate(string email, string password);

        Task<CustomUser> FindById(int id);

        Task<CustomUser> FindByEmail(string email);
    }
}