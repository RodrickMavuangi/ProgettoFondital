using Fondital.Shared;
using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class ConfigurazioneClient
    {
        private readonly HttpClient httpClient;

        public ConfigurazioneClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Configurazione>> GetAllConfigurazioni() =>
            await httpClient.GetFromJsonAsync<IEnumerable<Configurazione>>("configurazioniControl");

        public async Task UpdateConfigurazione(Configurazione configurazione) =>
            await httpClient.PostAsJsonAsync("configurazioniControl/update", configurazione);
    }
}
