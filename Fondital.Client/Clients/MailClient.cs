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
		private readonly AuthClient _authClient;
		public MailClient(HttpClient httpClient, AuthClient authClient)
		{
			this.httpClient = httpClient;
			_authClient = authClient;
		}

		public async Task sendMail(MailRequest mailRequest)
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync("MailController", mailRequest, JsonSerializerOpts.JsonOpts);
				response.EnsureSuccessStatusCode();
				//var result = await response.Content.ReadFromJsonAsync<Utente>(JsonSerializerOpts.JsonOpts);
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
				if(servicePartner.Id != 0)
				{
					// Utente con ServicePartner come Ruolo
					
					int servicePartnerId = servicePartner.Id;
					var response = await httpClient.PostAsJsonAsync($"MailController/{servicePartnerId}", utente, JsonSerializerOpts.JsonOpts);
					await _authClient.CreaRuolo(utente.UserName, new RuoloDto() { Name = "service partner" });
					response.EnsureSuccessStatusCode();
				}
				else
				{
					//Utente con Direzione come Ruolo

					var response = await httpClient.PostAsJsonAsync($"MailController/direzione", utente, JsonSerializerOpts.JsonOpts);
					await _authClient.CreaRuolo(utente.UserName, new RuoloDto() { Name = "direzione" });
					response.EnsureSuccessStatusCode();
				}
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}