using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Fondital.Client.Authentication
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private static readonly string _tokenkey = "TOKENKEY";

        public TokenHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>(_tokenkey);
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
