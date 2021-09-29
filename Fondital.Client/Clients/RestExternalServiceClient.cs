using Fondital.Shared.Dto;
using Fondital.Shared.Models;
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

		public async Task ModelloCaldaiaService()
		{
			try
			{
				var response = await httpClient.GetAsync($"externalServiceController/modelloCaldaia");
				if(response.StatusCode.ToString().Equals("NotFound"))
				{
					// Restituire un feedback du errore
				}
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