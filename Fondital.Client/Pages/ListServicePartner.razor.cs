using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Telerik.Blazor.Components;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Fondital.Shared.Services;
using Fondital.Shared;
using Fondital.Services;
using Fondital.Shared.Models.Auth;
using Fondital.Client.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Fondital.Client.Pages
{
	public class ListServicePartnerBase : ComponentBase
	{
		public List<ServicePartner> ServicePartners { get; set; } = new List<ServicePartner>();
		public ServicePartner ServicePartnerModel { get; set; } = new ServicePartner();
		public bool WindowVisible { get; set; }
		public bool ValidSubmit { get; set; } = false;
		public EditContext myEditContext { get; set; }

		public List<string> SearchableFields = new List<string> { "RagioneSociale" };
		[Inject] public ServicePartnerClient servicePartnerClient { get; set; }

		public string SearchText = "";
		protected override async Task OnInitializedAsync()
		{
			myEditContext = new EditContext(ServicePartnerModel);
			ServicePartners = (List<ServicePartner>)await servicePartnerClient.GetAllServicePartners();
		}
		public List<ServicePartner> ServicePartners_filtered => ServicePartners.Where<ServicePartner>(x => x.RagioneSociale.Contains(SearchText)).ToList();


		public async Task EditHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;
			//TODO Logic ....
		}

		public async Task UpdateHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;
			await servicePartnerClient.UpdateServicePartner(item.Id,item);
			await Refresh();
			
			// TODO Logic.....

		}

		public async Task DeleteHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;

		}

		public async Task CreateHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;

		}

		public async Task CancelHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;

			// if necessary, perform actual data source operation here through your service

		}

		async Task Refresh() 
		{
			WindowVisible = false;
			await OnInitializedAsync();
		}

		public async Task OnSubmitHandlerAsync(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();

			if (isFormValid)
			{
				ServicePartner ServicePartnerToSave = (ServicePartner)editContext.Model;
				await servicePartnerClient.CreateServicePartner(ServicePartnerToSave);
				await Refresh();
			}
			else
			{
				//apply some custom logic when the form is not valid
			}
		}
	}
}
