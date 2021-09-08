using Fondital.Shared;
using Fondital.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<IEnumerable<ServicePartner>> GetAllServicePartners()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ServicePartner>>("servicePartnersControl", JsonSerializerOpts.JsonOpts);
        }
        public async Task CreateServicePartner(ServicePartner servicePartner)
        {
            var response = await httpClient.PostAsJsonAsync($"servicePartnersControl", servicePartner, JsonSerializerOpts.JsonOpts);
            //response.EnsureSuccessStatusCode();
            //var result = await response.Content.ReadFromJsonAsync<ServicePartner>();
            //return result;
        }

        public async Task UpdateServicePartner(int id, ServicePartner servicePartner) =>
            await httpClient.PutAsJsonAsync($"servicePartnersControl/{id}", servicePartner, JsonSerializerOpts.JsonOpts);

        public async Task<ServicePartner> GetServicePartnerById(int id) =>
            await httpClient.GetFromJsonAsync<ServicePartner>($"servicePartnersControl/{id}", JsonSerializerOpts.JsonOpts);
        
		public async Task<ServicePartner> GetServicePartnerWithUtenti(int id) =>
		    await httpClient.GetFromJsonAsync<ServicePartner>($"servicePartnersControl/utenti/{id}", JsonSerializerOpts.JsonOpts);
	}
}