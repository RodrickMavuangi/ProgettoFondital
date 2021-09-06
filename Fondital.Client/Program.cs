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
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;

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
            builder.Services.AddHttpClient<ServicePartnerClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<ConfigurazioneClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<DifettoClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<VoceCostoClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<ListinoClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            builder.Services.AddHttpClient<MailClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));          

            builder.Services.AddHttpClient<LavorazioneClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]));
            //builder.Services.AddScoped<RemoteAuthenticationState, FonditalAuthenticationState>();
            builder.Services.AddScoped<FonditalAuthenticationState>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            
            builder.Services.AddOidcAuthentication<FonditalAuthenticationState, RemoteUserAccount>(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            });
            
            builder.Services.AddTelerikBlazor();

            builder.Services.AddLocalization(option => option.ResourcesPath = "LanguageResources");

            var host = builder.Build();

            CultureInfo culture;
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var result = await js.InvokeAsync<string>("blazorCulture.get");

            if (result != null)
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("it-IT");
                await js.InvokeVoidAsync("blazorCulture.set", "it-IT");
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            await host.RunAsync();
        }
    }
}
