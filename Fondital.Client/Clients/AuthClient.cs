using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Fondital.Client.Clients
{
    public class AuthClient
    {
        private readonly HttpClient httpClient;

        public AuthClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync($"authControl/login", loginRequest, JsonSerializerOpts.JsonOpts);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            return result;
        }

        public async Task ChangePassword(ChangePwRequestDto ChangeRequest)
        {
            var response = await httpClient.PostAsJsonAsync($"authControl/changepw", ChangeRequest, JsonSerializerOpts.JsonOpts);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        public async Task ResetPassword(ResetPwRequestDto ResetRequest)
        {
            var response = await httpClient.PostAsJsonAsync($"authControl/resetpw", ResetRequest, JsonSerializerOpts.JsonOpts);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
        
        public async Task ForgotPassword(ForgotPwRequestDto ForgotRequest)
        {
            MailRequest _mailRequest = new MailRequest() { Subject = "PASSWORD DIMENTICATA" , ToEmail = ForgotRequest.Email};
            var response = await httpClient.PostAsJsonAsync($"MailController", _mailRequest, JsonSerializerOpts.JsonOpts);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        public async Task AssegnaRuolo(string UtenteEmail, RuoloDto ruolo)
		{
                var response = await httpClient.PostAsJsonAsync<RuoloDto>($"authControl/ruolo/{UtenteEmail}",ruolo,JsonSerializerOpts.JsonOpts);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
        }
    }
}
