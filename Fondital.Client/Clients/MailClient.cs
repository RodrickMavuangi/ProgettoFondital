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

		public async Task sendMailForNewUser(UtenteDto utente)
		{
			try
			{
				if(utente.ServicePartner.Id != 0)
				{
					// Utente con ServicePartner come Ruolo

					//utente.Ruoli.Add(new RuoloDto() { Name = "Service Partner" });
					var response = await httpClient.PostAsJsonAsync("MailController/NewUser", utente, JsonSerializerOpts.JsonOpts);
					response.EnsureSuccessStatusCode();
					await _authClient.AssegnaRuolo(utente.UserName, new RuoloDto() { Name = "Service Partner" });
				}
				else
				{
					//Utente con Direzione come Ruolo

					//utente.Ruoli.Add(new RuoloDto() { Name = "Direzione" });
					var response = await httpClient.PostAsJsonAsync("MailController/NewUser", utente, JsonSerializerOpts.JsonOpts);
					response.EnsureSuccessStatusCode();
					await _authClient.AssegnaRuolo(utente.UserName, new RuoloDto() { Name = "Direzione" });
				}
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}