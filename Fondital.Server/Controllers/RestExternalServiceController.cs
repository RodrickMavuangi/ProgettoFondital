using Fondital.Shared.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Fondital.Server.Controllers
{
	[ApiController]
	[Route("externalServiceController")]
	[Authorize]
	public class RestExternalServiceController : ControllerBase
	{
		private readonly HttpClient _httpClient;
		private readonly RestClientSettings _restClientSettings;
		public RestExternalServiceController( HttpClient httpClient, IOptions<RestClientSettings> restClientSettings)
		{
			_httpClient = httpClient;
			_restClientSettings = restClientSettings.Value;
		}

		[HttpGet("modelloCaldaia")]
		[AllowAnonymous]
		public async Task<IActionResult> GetServiceCaldaia()
		{
			try
			{
				_httpClient.BaseAddress = new Uri(_restClientSettings.BaseAddress);
				//_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var response = _httpClient.GetAsync(_restClientSettings.UriModelloCaldaia).Result;
				if (!response.IsSuccessStatusCode)
					return NotFound();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok(); ;
		}

		[HttpGet("prezzoPezzoRicambio")]
		[AllowAnonymous]
		public async Task<IActionResult> GetServiceRicambio()
		{
			try
			{
				_httpClient.BaseAddress = new Uri(_restClientSettings.BaseAddress);
				var response = _httpClient.GetAsync(_restClientSettings.UriRicambio).Result;
				if (!response.IsSuccessStatusCode)
					return NotFound();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok(); ;
		}
	}

}