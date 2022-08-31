using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace UdemyIdentityServer.AuthServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          var  rsa = RSA.Create();
          
              rsa.ImportRSAPrivateKey(
              Convert.FromBase64String(Configuration["Jwt:Asymmetric:PrivateKey"]),
              out _);

              rsa.ImportRSAPublicKey(
                  Convert.FromBase64String(Configuration["Jwt:Asymmetric:PublicKey"]),
                  out _
              );

            
             


            services.AddIdentityServer()
                  .AddInMemoryApiResources(Config.GetApiResources())
                  .AddInMemoryApiScopes(Config.GetApiScopes())
                  .AddInMemoryClients(Config.GetClients())
                  .AddSigningCredential( new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256// Important to use RSA version of the SHA algo 
                  );
                  

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}