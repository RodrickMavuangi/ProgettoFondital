using Fondital.Client.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            await builder.Build().RunAsync();
        }
    }
}
