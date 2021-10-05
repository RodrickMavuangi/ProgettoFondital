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
        public MailClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task sendMail(MailRequestDto mailRequest)
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
                var response = await httpClient.PostAsJsonAsync($"MailController/addUser", utente, JsonSerializerOpts.JsonOpts);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}