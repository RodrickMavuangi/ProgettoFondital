using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Fondital.Client.Pages
{
    public partial class ListServicePartner
    {
        public List<ServicePartnerDto> ServicePartners;
        public ServicePartnerDto ServicePartnerModel { get; set; } = new ServicePartnerDto();
        public bool WindowVisible { get; set; }
        public EditContext myEditContext { get; set; }
        public List<string> SearchableFields = new List<string> { "RagioneSociale" };
        public string SearchText = "";
        public bool myEditTemplate { get; set; } = false;
        ServicePartnerDto DatiSP = new ServicePartnerDto();
        protected ServicePartnerDto SpSelected { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        public List<ServicePartnerDto> ServicePartners_filtered => ServicePartners.Where(x => x.RagioneSociale.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

        protected override async Task OnInitializedAsync()
        {
            myEditContext = new EditContext(ServicePartnerModel);
            ServicePartners = (List<ServicePartnerDto>)await servicePartnerClient.GetAllServicePartners();
        }

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
    }
}