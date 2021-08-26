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
    public class ServicePartnerClient
    {
        private readonly HttpClient httpClient;

        public ServicePartnerClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ServicePartner>> GetAllServicePartners() =>
            await httpClient.GetFromJsonAsync<IEnumerable<ServicePartner>>("servicePartners");

        public async Task<ServicePartner> CreateServicePartner(ServicePartner servicePartner)
        {
            var response = await httpClient.PostAsJsonAsync($"servicePartners", servicePartner);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ServicePartner>();
            return result;
        }

        public async Task<ServicePartner> UpdateServicePartner(int id,ServicePartner servicePartner)
		{
            var response = await httpClient.PutAsJsonAsync($"servicePartners/{id}", servicePartner);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ServicePartner>();
            return result;
		}
    }
}
