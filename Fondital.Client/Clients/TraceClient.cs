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
            await httpClient.GetFromJsonAsync<IEnumerable<Trace>>("traces");

        //public async Task CreateDummyTrace(string traceDescription)
        //{
        //    var response =
        //    await httpClient.PostAsJsonAsync<Trace>("traces", new Trace
        //    {
        //        Tipologia = TraceType.LoginInfo,
        //        Descrizione = traceDescription,
        //        //Utente = GetUtenteById(1)
        //        Utente = new Utente { Id = 1}
        //    });
        //
        //    response.EnsureSuccessStatusCode();
        //    if (await response.Content.ReadFromJsonAsync<int>() == 0)
        //        throw new Exception("errore nella chiamata al server");
        //        
        //}
    }
}
