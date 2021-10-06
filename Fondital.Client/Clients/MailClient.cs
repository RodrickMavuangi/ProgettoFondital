using Fondital.Shared.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task sendMail(MailRequestDto mailRequest)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("MailController", mailRequest, JsonSerializerOpts.JsonOpts);
                response.EnsureSuccessStatusCode();
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
				if(utente.ServicePartner != null)
				{
					// Utente con ServicePartner come Ruolo

					var response = await httpClient.PostAsJsonAsync("MailController/NewUser", utente, JsonSerializerOpts.JsonOpts);
					response.EnsureSuccessStatusCode();
					await _authClient.AssegnaRuolo(utente.UserName, new RuoloDto() { Name = "Service Partner" });
				}
				else
				{
					//Utente con Direzione come Ruolo

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