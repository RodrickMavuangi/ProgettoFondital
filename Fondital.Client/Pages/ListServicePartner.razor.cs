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
		public EditContext myEditContext_UpdateSP { get; set; }
		public ServicePartner ServicePartnerModel_UpdateSP { get; set; } = new ServicePartner() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };

		public List<string> SearchableFields = new List<string> { "RagioneSociale" };
		[Inject] public ServicePartnerClient servicePartnerClient { get; set; }
		[Parameter] public EventCallback OnClose { get; set; }

		public string SearchText = "";
		public bool myEditTemplate { get; set; } = false;
		ServicePartner DatiSP = new ServicePartner();
		public ServicePartner ServicePartnerToSave { get; set; } = new ServicePartner(); 
		protected override async Task OnInitializedAsync()
		{
			myEditContext = new EditContext(ServicePartnerModel);
			ServicePartners = (List<ServicePartner>)await servicePartnerClient.GetAllServicePartners();

			myEditContext_UpdateSP = new EditContext(ServicePartnerModel_UpdateSP);
		}
		public List<ServicePartner> ServicePartners_filtered => ServicePartners.Where<ServicePartner>(x => x.RagioneSociale.Contains(SearchText)).ToList();

		

		public async Task EditHandler(GridCommandEventArgs args)
		{
			myEditTemplate = true;
			DatiSP = (ServicePartner)args.Item;
			ServicePartnerModel_UpdateSP = await servicePartnerClient.GetServicePartnerWithUtenti(DatiSP.Id);
			ServicePartnerModel_UpdateSP.RagioneSociale = DatiSP.RagioneSociale;
			ServicePartnerModel_UpdateSP.CodiceCliente = DatiSP.CodiceCliente;
			ServicePartnerModel_UpdateSP.CodiceFornitore = DatiSP.CodiceFornitore;
		}

		public async Task EditSP_UpdateHandler(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();
			if (isFormValid)
			{
				ServicePartner ServicePartnerToSave = (ServicePartner)editContext.Model;

				await servicePartnerClient.UpdateServicePartner(DatiSP.Id, ServicePartnerToSave);
				await Refresh();
				DatiSP = new ServicePartner();
			}
			else
			{

			}	
		}


		public async Task UpdateHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;
			await servicePartnerClient.UpdateServicePartner(item.Id,item);
			await Refresh();
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
				ServicePartnerToSave = (ServicePartner)editContext.Model;
				await servicePartnerClient.CreateServicePartner(ServicePartnerToSave);
				ServicePartnerModel = new ServicePartner();
				ServicePartnerToSave = new ServicePartner();
				await Refresh();
			}
			else
			{
				//apply some custom logic when the form is not valid
			}
		}

		public void Navigate(GridRowClickEventArgs args)
		{
			ServicePartner utente = (ServicePartner)args.Item;
			//NavigationManager.NavigateTo("/detailservicepartner");
		}
	}
}
