using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fondital.Client.Authentication
{
    public class FonditalAuthStateProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private static readonly string _tokenkey = "TOKENKEY";
        private static AuthenticationState Anonymous => new(new ClaimsPrincipal(new ClaimsIdentity()));

        public FonditalAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
        {
            this._localStorage = localStorage;
            this._httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(_tokenkey);
            if (!TokenStatus(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                await _localStorage.RemoveItemAsync(_tokenkey);
                return Anonymous;
            }
            return BuildAuthenticationState(token);
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsync(_tokenkey, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            await _localStorage.RemoveItemAsync(_tokenkey);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ClaimsFromJwt(token), "jwt")));
        }

        private static bool TokenStatus(string token)
        {
            try
            {
                if (String.IsNullOrEmpty(token))
                    return false;

                var jwthandler = new JwtSecurityTokenHandler();
                var jwttoken = jwthandler.ReadJwtToken(token);
                return jwttoken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }

        private static IEnumerable<Claim> ClaimsFromJwt(string token)
        {
            try
            {
                if (String.IsNullOrEmpty(token))
                    return null;

                var jwthandler = new JwtSecurityTokenHandler();
                var jwttoken = jwthandler.ReadJwtToken(token);
                return jwttoken.Claims;
            }
            catch
            {
                return null;
            }
        }
    }
}