using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class TraceClient
    {
        private readonly HttpClient httpClient;

        public TraceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Trace>> GetTraces() =>
            await httpClient.GetFromJsonAsync<IEnumerable<Trace>>("tracesControl");

    }
}
