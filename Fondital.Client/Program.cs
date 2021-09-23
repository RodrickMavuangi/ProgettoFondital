using Blazored.LocalStorage;
using Fondital.Client.Authentication;
using Fondital.Client.Clients;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.JSInterop;
using System;
using System.Globalization;
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
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"]) });

            builder.Services.AddScoped<TokenHandler>();
            builder.Services.AddHttpClient<UtenteClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<ServicePartnerClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<ConfigurazioneClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<DifettoClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<VoceCostoClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<ListinoClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<MailClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<AuthClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddHttpClient<LavorazioneClient>(client => client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"])).AddHttpMessageHandler<TokenHandler>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<FonditalAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, FonditalAuthStateProvider>(provider => provider.GetRequiredService<FonditalAuthStateProvider>());
            builder.Services.AddScoped<ILoginService, FonditalAuthStateProvider>(provider => provider.GetRequiredService<FonditalAuthStateProvider>());
            builder.Services.AddOptions();

            builder.Services.AddTelerikBlazor();
            builder.Services.AddLocalization(option => option.ResourcesPath = "LanguageResources");

            ConfigureLogging(builder);

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

        private static void ConfigureLogging(WebAssemblyHostBuilder builder, string section = "Logging")
        {
            builder.Logging.AddConfiguration(builder.Configuration.GetSection(section));
            builder.Logging.ClearProviders();
            builder.Logging.AddAzureWebAppDiagnostics();
            builder.Services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "fondital-azure-diagnostics-client";
                options.FileSizeLimit = 5 * 1024;
                options.RetainedFileCountLimit = 5;
            });
        }
    }
}