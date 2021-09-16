using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
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
	public class MailClient
	{
		private readonly HttpClient httpClient;
		public MailClient(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task sendMail(MailRequest mailRequest)
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync("MailController", mailRequest, JsonSerializerOpts.JsonOpts);
				response.EnsureSuccessStatusCode();
				var result = await response.Content.ReadFromJsonAsync<Utente>(JsonSerializerOpts.JsonOpts);
			}
			catch (Exception e)
			{
				throw;
			}
		}

		public async Task sendMailForNewUser(UtenteDto utente, ServicePartnerDto servicePartner)
		{
			try
			{
				int servicePartnerId = servicePartner.Id;
				var response = await httpClient.PostAsJsonAsync($"MailController/{servicePartnerId}", utente, JsonSerializerOpts.JsonOpts);
				response.EnsureSuccessStatusCode();
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}