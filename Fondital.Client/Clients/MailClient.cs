using Fondital.Shared.Models;
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
				var response = await httpClient.PostAsJsonAsync<MailRequest>("sendMail", mailRequest);
				response.EnsureSuccessStatusCode();
				var result = await response.Content.ReadFromJsonAsync<ServicePartner>();
			}
			catch(Exception e) { var tu = e.Message; }			
		}
	}
}