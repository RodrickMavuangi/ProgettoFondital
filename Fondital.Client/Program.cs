using Fondital.Client.Clients;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Fondital.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<UtenteClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<TraceClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            //builder.Services.AddScoped<AuthenticationState, FonditalAuthenticationState>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            //builder.Services.AddApiAuthorization(options => options.AuthenticationPaths.LogOutSucceededPath = "");
            //builder.Services.AddApiAuthorization<FonditalAuthenticationState>(options => options.AuthenticationPaths.LogOutSucceededPath = "");
            //builder.Services.AddApiAuthorization();
            builder.Services.AddOidcAuthentication<FonditalAuthenticationState, RemoteUserAccount>(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            });

            await builder.Build().RunAsync();
        }
    }
}
