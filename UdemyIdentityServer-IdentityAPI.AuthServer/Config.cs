// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace UdemyIdentityServer_IdentityAPI.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){
                    Scopes={ "api1.read","api1.write","api1.update" },
                    ApiSecrets = new []{new  Secret("secretapi1".Sha256())
                    }
                },
                new ApiResource("resource_api2")
                {
                       Scopes={ "api2.read","api2.write","api2.update" },
                          ApiSecrets = new []{new  Secret("secretapi2".Sha256()) }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read","API 1 için okuma izni"),
                  new ApiScope("api1.write","API 1 için yazma izni"),
                    new ApiScope("api1.update","API 1 için güncelleme izni"),
                       new ApiScope("api2.read","API 2 için okuma izni"),
                  new ApiScope("api2.write","API 2 için yazma izni"),
                    new ApiScope("api2.update","API 2 için güncelleme izni"),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(), //subId
                new IdentityResources.Profile(), ///
                new IdentityResource(){ Name="CountryAndCity", DisplayName="Country and City",Description="Kullanıcının ülke ve şehir bilgisi", UserClaims= new [] {"country","city"}},

                new IdentityResource(){ Name="Roles",DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new [] { "role"} }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>(){
                new Client()
                {
                    ClientId = "Client1",
                    ClientName="Client 1 app uygulaması",
                   ClientSecrets=new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.ClientCredentials,
                   AllowedScopes= {"api1.read"}
                },
                 new Client()
                {
                    ClientId = "Client2",
                    ClientName="Client 2 app uygulaması",
                   ClientSecrets=new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.ClientCredentials,
                   AllowedScopes= {"api1.read" ,"api1.update","api2.write","api2.update"}
                },
                 new Client()
                 {
                     ClientId = "Client1-Mvc",
                     RequirePkce=false,
                    ClientName="Client 1 app  mvc uygulaması",
                   ClientSecrets=new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.Hybrid,
                   RedirectUris=new  List<string>{ "https://localhost:5006/signin-oidc" },
                   PostLogoutRedirectUris=new List<string>{ "https://localhost:5006/signout-callback-oidc" },
                   AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                   AccessTokenLifetime=2*60*60,
                   AllowOfflineAccess=true,
                   RefreshTokenUsage=TokenUsage.ReUse,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,

                   RequireConsent=false
        },

                  new Client()
                 {
                     ClientId = "Client2-Mvc",
                     RequirePkce=false,
                    ClientName="Client 2 app  mvc uygulaması",
                   ClientSecrets=new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.Hybrid,
                   RedirectUris=new  List<string>{ "https://localhost:5011/signin-oidc" },
                   PostLogoutRedirectUris=new List<string>{ "https://localhost:5011/signout-callback-oidc" },
                   AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read","api2.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                   AccessTokenLifetime=2*60*60,
                   AllowOfflineAccess=true,
                   RefreshTokenUsage=TokenUsage.ReUse,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,

                   RequireConsent=false
        },

                  new Client()
                  {
                      ClientId="js-client",
                      RequireClientSecret=false,
                      AllowedGrantTypes=GrantTypes.Code,
                     ClientName="Js Client (Angular)",
                      AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                      RedirectUris={"http://localhost:4200/callback"},
                      AllowedCorsOrigins={"http://localhost:4200"},
                      PostLogoutRedirectUris={"http://localhost:4200" }
                  },

                          new Client()
                 {
                     ClientId = "Client1-ResourceOwner-Mvc",

                    ClientName="Client 1 app  mvc uygulaması",
                   ClientSecrets=new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                   AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                   AccessTokenLifetime=2*60*60,
                   AllowOfflineAccess=true,
                   RefreshTokenUsage=TokenUsage.ReUse,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
        },
    };
        }
    }
}