using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Pages
{
    public partial class UtenzeDirezione
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        [Parameter]
        public string servicePId { get; set; }
        public string SearchText = "";
        private int PageSize { get; set; }
        public string StatusFilter { get; set; }
        public List<string> ListaScelta { get; set; }
        protected bool ShowAddDialog { get; set; } = false;
        protected bool ShowEditDialog { get; set; } = false;
        private List<UtenteDto> UtentiDirezione = new();
        protected UtenteDto UtenteSelected { get; set; }
        public List<UtenteDto> ListaUtDirezioneFiltered => StatusFilter == ListaScelta[0] ? UtentiDirezione.Where(x => x.UserName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase) && x.IsAbilitato == true).ToList() :
                                                           StatusFilter == ListaScelta[1] ? UtentiDirezione.Where(x => x.UserName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase) && x.IsAbilitato == false).ToList() :
                                                           UtentiDirezione.Where(x => x.UserName.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();

        protected override async Task OnInitializedAsync()
        {
            ListaScelta = new() { localizer["Abilitati"], localizer["Disabilitati"] };
            PageSize = Convert.ToInt32(config["PageSize"]);

            await RefreshUtenti();
        }

        protected async Task CloseAndRefresh()
        {
            ShowAddDialog = false;
            ShowEditDialog = false;
            await RefreshUtenti();
        }

        protected async Task RefreshUtenti()
        {
            UtentiDirezione = (List<UtenteDto>)await utenteClient.GetUtenti("Direzione");
            StateHasChanged();
        }

        protected void EditUtente(string username)
        {
            UtenteSelected = UtentiDirezione.Single(x => x.UserName == username);
            ShowEditDialog = true;
        }

        protected async Task SendMail(string username)
        {
            UtenteSelected = UtentiDirezione.Single(x => x.UserName == username);
            
            bool isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ResetPassword"]} {UtenteSelected.UserName}?", " ");
            if (isConfirmed)
            {
                MailRequestDto mailRequest = new MailRequestDto()
                {
                    ToEmail = UtenteSelected.UserName,
                    Subject = localizer["RisettaPassword"],
                };

                await mailClient.sendMail(mailRequest);
                await Dialogs.AlertAsync($"{@localizer["MailInviata"]} {UtenteSelected.UserName}.", " ");
            }
        }

        protected async Task UpdateEnableUtente(string username)
        {
            UtenteSelected = UtentiDirezione.Single(x => x.UserName == username);

            bool isConfirmed = false;
            if (UtenteSelected.IsAbilitato)
                isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ConfermaAbilitazione"]} {localizer["Utente"]} {UtenteSelected.UserName}", " ");
            else
                isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ConfermaDisabilitazione"]} {localizer["Utente"]} {UtenteSelected.UserName}", " ");

            if (isConfirmed)
            {
                try
                {
                    await utenteClient.UpdateUtente(UtenteSelected);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                //	//fai revert: ^ restituisce lo XOR dei due valori
                //	//true XOR true = false
                //	//false XOR true = true kjkj
                UtenteSelected.IsAbilitato ^= true;
            }
        }
    }
}