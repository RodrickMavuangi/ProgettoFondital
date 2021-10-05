using Fondital.Shared.Dto;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ServicePartnerDto>> GetAllServicePartners()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ServicePartnerDto>>("servicePartnersControl", JsonSerializerOpts.JsonOpts);
        }
        public async Task CreateServicePartner(ServicePartnerDto servicePartner)
        {
            var response = await httpClient.PostAsJsonAsync($"servicePartnersControl", servicePartner, JsonSerializerOpts.JsonOpts);
			response.EnsureSuccessStatusCode();
			//var result = await response.Content.ReadFromJsonAsync<ServicePartnerDto>(JsonSerializerOpts.JsonOpts);
			//return result;
		}

        public async Task UpdateServicePartner(ServicePartnerDto servicePartner)
        {
           var response = await httpClient.PutAsJsonAsync($"servicePartnersControl/{servicePartner.Id}", servicePartner, JsonSerializerOpts.JsonOpts);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<ServicePartnerDto> GetServicePartnerById(int id) =>
            await httpClient.GetFromJsonAsync<ServicePartnerDto>($"servicePartnersControl/{id}", JsonSerializerOpts.JsonOpts);
        
		public async Task<ServicePartnerDto> GetServicePartnerWithUtenti(int id) =>
		    await httpClient.GetFromJsonAsync<ServicePartnerDto>($"servicePartnersControl/utenti/{id}", JsonSerializerOpts.JsonOpts);
	}
}