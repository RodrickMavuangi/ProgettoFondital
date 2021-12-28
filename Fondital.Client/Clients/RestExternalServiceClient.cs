using Fondital.Client.Utils;
using Fondital.Shared.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class RestExternalServiceClient
    {
        private readonly HttpClient httpClient;
        public RestExternalServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CaldaiaDto> GetCaldaia(CaldaiaDto caldaiaDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"externalServiceController/modelloCaldaia", caldaiaDto, JsonSerializerOpts.JsonOpts);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<CaldaiaDto>(JsonSerializerOpts.JsonOpts);
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new Exception("NotFound");
                else
                    throw new Exception("GenericError");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ServicePartnerDto> GetDettagliSP(ServicePartnerRequestDto sp)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"externalServiceController/dettagliSP", sp, JsonSerializerOpts.JsonOpts);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<ServicePartnerDto>(JsonSerializerOpts.JsonOpts);
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new Exception("NotFound");
                else
                    throw new Exception("GenericError");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<RicambioDto> GetPezzoRicambio(RicambioRequestDto ricambioRequest)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"externalServiceController/pezzoRicambio", ricambioRequest, JsonSerializerOpts.JsonOpts);
                if (response.IsSuccessStatusCode)
                      return await response.Content.ReadFromJsonAsync<RicambioDto>(JsonSerializerOpts.JsonOpts);
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new Exception("NotFound");
                else
                    throw new Exception("GenericError");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}