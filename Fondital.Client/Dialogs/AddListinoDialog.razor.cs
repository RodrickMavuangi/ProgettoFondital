using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Dialogs
{
    public partial class AddListinoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected ListinoDto NuovoListino { get; set; } = new();
        protected List<ServicePartnerDto> ServicePartners = new();
        protected List<VoceCostoDto> VociCosto = new();
        protected List<ListinoDto> Listini = new();
        protected int SpIdSelected { get => NuovoListino.ServicePartner?.Id ?? 0; set { UpdateSelectedSp(value); } }
        protected int VcIdSelected { get => NuovoListino.VoceCosto?.Id ?? 0; set { UpdateSelectedVc(value); } }
        private string CurrentCulture { get; set; }
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            CurrentCulture = await StateProvider.GetCurrentCulture();
            ServicePartners = (List<ServicePartnerDto>)await spClient.GetAllServicePartners();
            VociCosto = (List<VoceCostoDto>)await vcClient.GetAllVociCosto();
            Listini = (List<ListinoDto>)await listinoClient.GetAllListini();

            NuovoListino.ServicePartner = ServicePartners.First();
            NuovoListino.VoceCosto = VociCosto.First();
        }

        protected async Task SalvaListino()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                if (Listini.Exists(x => x.Raggruppamento == NuovoListino.Raggruppamento && x.ServicePartner.Id == NuovoListino.ServicePartner.Id && x.VoceCosto.Id == NuovoListino.VoceCosto.Id))
                    throw new Exception("ErroreListino");
                await listinoClient.CreateListino(NuovoListino);
                isSubmitting = false;
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                ErrorMessage = localizer[ex.Message];
            }
        }

        protected void UpdateSelectedSp(int value)
        {
            NuovoListino.ServicePartner = ServicePartners.Single(sp => sp.Id == value);
        }

        protected void UpdateSelectedVc(int value)
        {
            NuovoListino.VoceCosto = VociCosto.Single(vc => vc.Id == value);
        }
    }
}