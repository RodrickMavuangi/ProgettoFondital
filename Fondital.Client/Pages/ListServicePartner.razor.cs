using Fondital.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class ListServicePartner
    {
        public List<ServicePartnerDto> ServicePartners;
        public string SearchText = "";
        protected ServicePartnerDto SpSelected { get; set; } = new();
        protected ServicePartnerDto SpDellaLista { get; set; } = new();

        private int PageSize { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        public List<ServicePartnerDto> ServicePartners_filtered => ServicePartners.Where(x => x.RagioneSociale.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

        protected override async Task OnInitializedAsync()
        {
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshSP();
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
            SpDellaLista = ServicePartners.Single(x => x.Id == SpId);
            SpSelected = new ServicePartnerDto()
            {
                BankCode = SpDellaLista.BankCode,
                Id = SpDellaLista.Id,
                CC = SpDellaLista.CC,
                KPP = SpDellaLista.KPP,
                Listini = SpDellaLista.Listini,
                CCC = SpDellaLista.CCC,
                City = SpDellaLista.City,
                CodiceCliente = SpDellaLista.CodiceCliente,
                Name = SpDellaLista.Name,
                BankName = SpDellaLista.BankName,
                CodiceFornitore = SpDellaLista.CodiceFornitore,
                ContractDate = SpDellaLista.ContractDate,
                ContractNr = SpDellaLista.ContractNr,
                Email = SpDellaLista.Email,
                HouseNr = SpDellaLista.HouseNr,
                INN = SpDellaLista.INN,
                ManagerName = SpDellaLista.ManagerName,
                Phone = SpDellaLista.Phone,
                PostalCode = SpDellaLista.PostalCode,
                RagioneSociale = SpDellaLista.RagioneSociale,
                Region = SpDellaLista.Region,
                StateRegistrationNr = SpDellaLista.StateRegistrationNr,
                Street = SpDellaLista.Street,
                Utenti = SpDellaLista.Utenti
            };
            ShowEditDialog = true;
        }
    }
}