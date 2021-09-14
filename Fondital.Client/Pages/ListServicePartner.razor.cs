using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Fondital.Client.Clients;
using System;

namespace Fondital.Client.Pages
{
	public partial class ListServicePartner
	{
		public List<ServicePartner> ServicePartners { get; set; } = new List<ServicePartner>();
		public ServicePartner ServicePartnerModel { get; set; } = new ServicePartner();
		public bool WindowVisible { get; set; }
		public bool ValidSubmit { get; set; } = false;
		public EditContext myEditContext { get; set; }
		public EditContext myEditContext_UpdateSP { get; set; }
		public ServicePartner ServicePartnerModel_UpdateSP { get; set; } = new ServicePartner() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };

		public List<string> SearchableFields = new List<string> { "RagioneSociale" };

		public string SearchText = "";
		public bool myEditTemplate { get; set; } = false;

		ServicePartner DatiSP = new ServicePartner();
		public ServicePartner ServicePartnerToSave { get; set; } = new ServicePartner();

		// --codice temporaneo--
		protected ServicePartner SpSelected { get; set; }
		protected bool ShowAddDialog { get; set; } = false;

		protected bool ShowEditDialog { get; set; } = false;

		// --fine codice temporaneo--


		protected override async Task OnInitializedAsync()
		{
			myEditContext = new EditContext(ServicePartnerModel);
			ServicePartners = (List<ServicePartner>)await servicePartnerClient.GetAllServicePartners();

			//myEditContext_UpdateSP = new EditContext(ServicePartnerModel_UpdateSP);
		}
		public List<ServicePartner> ServicePartners_filtered => ServicePartners.Where<ServicePartner>(x => x.RagioneSociale.ToLower().Contains(SearchText.ToLower())).ToList();

		// --codice temporaneo--
		protected async Task RefreshSP()
		{
			ServicePartners = (List<ServicePartner>)await servicePartnerClient.GetAllServicePartners();
			StateHasChanged();
		}

		protected async Task CloseAndRefresh()
		{
			ShowAddDialog = false;
			ShowEditDialog = false;
			await RefreshSP();
		}

		protected void EditSp(int SpId)
        {
			SpSelected = ServicePartners.Single(x => x.Id == SpId);
			ShowEditDialog = true;
        }

		// --fine codice temporaneo--

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
				DatiSP = new ServicePartner();
				await Refresh();
			}

			else
			{

			}
		}


		public async Task UpdateHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;
			await servicePartnerClient.UpdateServicePartner(item.Id, item);
			await Refresh();
		}
		async Task Refresh()
		{
			myEditTemplate = false;
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
	}
}
