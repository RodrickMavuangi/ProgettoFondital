using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Fondital.Client.Pages
{
    public partial class ListServicePartner
	{
		public List<ServicePartnerDto> ServicePartners { get; set; } = new List<ServicePartnerDto>();
		public ServicePartnerDto ServicePartnerModel { get; set; } = new ServicePartnerDto();
		public bool WindowVisible { get; set; }
		//public bool ValidSubmit { get; set; } = false;
		public EditContext myEditContext { get; set; }
		//public EditContext myEditContext_UpdateSP { get; set; }
		//public ServicePartnerDto ServicePartnerModel_UpdateSP { get; set; } = new ServicePartnerDto() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };

		public List<string> SearchableFields = new List<string> { "RagioneSociale" };

		public string SearchText = "";
		public bool myEditTemplate { get; set; } = false;

		ServicePartnerDto DatiSP = new ServicePartnerDto();
		//public ServicePartnerDto ServicePartnerToSave { get; set; } = new ServicePartnerDto();


		protected ServicePartnerDto SpSelected { get; set; }
		protected bool ShowAddDialog { get; set; } = false;

		protected bool ShowEditDialog { get; set; } = false;


		protected override async Task OnInitializedAsync()
		{
			myEditContext = new EditContext(ServicePartnerModel);
			ServicePartners = (List<ServicePartnerDto>)await servicePartnerClient.GetAllServicePartners();

			//myEditContext_UpdateSP = new EditContext(ServicePartnerModel_UpdateSP);
		}
		public List<ServicePartnerDto> ServicePartners_filtered => ServicePartners.Where<ServicePartnerDto>(x => x.RagioneSociale.ToLower().Contains(SearchText.ToLower())).ToList();


		protected async Task RefreshSP()
		{
			ServicePartners = (List<ServicePartnerDto>)await servicePartnerClient.GetAllServicePartners();
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


		//public async Task EditHandler(GridCommandEventArgs args)
		//{
		//	myEditTemplate = true;
		//	DatiSP = (ServicePartnerDto)args.Item;
		//	ServicePartnerModel_UpdateSP = await servicePartnerClient.GetServicePartnerWithUtenti(DatiSP.Id);
		//	ServicePartnerModel_UpdateSP.RagioneSociale = DatiSP.RagioneSociale;
		//	ServicePartnerModel_UpdateSP.CodiceCliente = DatiSP.CodiceCliente;
		//	ServicePartnerModel_UpdateSP.CodiceFornitore = DatiSP.CodiceFornitore;
		//}

		//public async Task EditSP_UpdateHandler(EditContext editContext)
		//{
		//	bool isFormValid = editContext.Validate();
		//	if (isFormValid)
		//	{
		//		ServicePartnerDto ServicePartnerToSave = (ServicePartnerDto)editContext.Model;

		//		await servicePartnerClient.UpdateServicePartner(DatiSP.Id, ServicePartnerToSave);
		//		DatiSP = new ServicePartnerDto();
		//		await Refresh();
		//	}

		//	else
		//	{

		//	}
		//}


		public async Task UpdateHandler(GridCommandEventArgs args)
		{
			ServicePartnerDto item = (ServicePartnerDto)args.Item;
			await servicePartnerClient.UpdateServicePartner(item.Id, item);
			await Refresh();
		}
		async Task Refresh()
		{
			myEditTemplate = false;
			WindowVisible = false;
			await OnInitializedAsync();
		}

		//public async Task OnSubmitHandlerAsync(EditContext editContext)
		//{
		//	bool isFormValid = editContext.Validate();

		//	if (isFormValid)
		//	{
		//		ServicePartnerToSave = (ServicePartnerDto)editContext.Model;
		//		await servicePartnerClient.CreateServicePartner(ServicePartnerToSave);
		//		ServicePartnerModel = new ServicePartnerDto();
		//		ServicePartnerToSave = new ServicePartnerDto();
		//		await Refresh();
		//	}
		//	else
		//	{
		//		//apply some custom logic when the form is not valid
		//	}
		//}
	}
}
