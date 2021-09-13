using System;
using System.Collections.Generic;
using Fondital.Shared.Resources;
using System.Linq;
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

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync($"authControl/login", loginRequest, JsonSerializerOpts.JsonOpts);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result;
        }
    }
}
