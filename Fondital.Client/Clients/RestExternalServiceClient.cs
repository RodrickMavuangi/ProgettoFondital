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

		public async Task<string> ModelloCaldaiaService(string matricola)
		{
			try
			{
				var response = await httpClient.GetAsync($"externalServiceController/modelloCaldaia/{matricola}");
				if (response.IsSuccessStatusCode)
					return await response.Content.ReadFromJsonAsync<string>(JsonSerializerOpts.JsonOpts);
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

		public async Task PezzoRicambioService()
		{
			try
			{
				var response = await httpClient.GetAsync($"externalServiceController/pezzoRicambio");
				if (response.StatusCode.ToString().Equals("NotFound"))
				{
					// Restituire un feedback du errore
				}
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}