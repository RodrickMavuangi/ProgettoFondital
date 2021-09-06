using Fondital.Client.Clients;
//using Fondital.Data;
using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fondital.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<UtenteClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<TraceClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<ServicePartnerClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<MailClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            //builder.Services.AddScoped<RemoteAuthenticationState, FonditalAuthenticationState>();
            builder.Services.AddScoped<FonditalAuthenticationState>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            //builder.Services.AddApiAuthorization<FonditalAuthenticationState>(options => options.AuthenticationPaths.LogOutSucceededPath = "");

            builder.Services.AddOidcAuthentication<FonditalAuthenticationState, RemoteUserAccount>(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            });
            
            builder.Services.AddTelerikBlazor();

            await builder.Build().RunAsync();
        }
    }
}
