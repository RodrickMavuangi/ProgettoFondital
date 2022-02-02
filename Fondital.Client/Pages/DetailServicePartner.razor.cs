using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;

namespace Fondital.Client.Pages
{
    public partial class DetailServicePartner
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
        protected bool ShowEditDialog_SP { get; set; } = false;
        protected UtenteDto UtenteSelected { get; set; }
        protected UtenteDto UtenteDellaLista { get; set; }

        protected ServicePartnerDto SpSelected { get; set; } = new();
        public List<UtenteDto> ListaUtentiFiltered => StatusFilter == ListaScelta[0] ? SpSelected.Utenti.Where(x => x.UserName.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && x.IsAbilitato == true).ToList() :
                                                      StatusFilter == ListaScelta[1] ? SpSelected.Utenti.Where(x => x.UserName.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && x.IsAbilitato == false).ToList() :
                                                      SpSelected.Utenti.Where(x => x.UserName.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();

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
            ShowEditDialog_SP = false;
            await RefreshUtenti();
        }

        protected async Task RefreshUtenti()
        {
            SpSelected = await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));
            StateHasChanged();
        }

        protected void EditUtente(string username)
        {
            UtenteDellaLista = SpSelected.Utenti.Single(x => x.UserName == username);
            UtenteSelected = new UtenteDto()
            {
                Cognome = UtenteDellaLista.Cognome,
                Nome = UtenteDellaLista.Nome,
                Email = UtenteDellaLista.Email,
                UserName = UtenteDellaLista.UserName,
                Id = UtenteDellaLista.Id,
                Rapporti = UtenteDellaLista.Rapporti,
                Ruoli = UtenteDellaLista.Ruoli,
                AccessFailedCount = UtenteDellaLista.AccessFailedCount,
                TwoFactorEnabled = UtenteDellaLista.TwoFactorEnabled,
                PasswordHash = UtenteDellaLista.PasswordHash,
                ConcurrencyStamp = UtenteDellaLista.ConcurrencyStamp,
                EmailConfirmed = UtenteDellaLista.EmailConfirmed,
                IsAbilitato = UtenteDellaLista.IsAbilitato,
                LockoutEnabled = UtenteDellaLista.LockoutEnabled,
                LockoutEnd = UtenteDellaLista.LockoutEnd,
                NormalizedEmail = UtenteDellaLista.NormalizedEmail,
                NormalizedUserName = UtenteDellaLista.NormalizedUserName,
                PhoneNumber = UtenteDellaLista.PhoneNumber,
                PhoneNumberConfirmed = UtenteDellaLista.PhoneNumberConfirmed,
                Pw_LastChanged = UtenteDellaLista.Pw_LastChanged,
                Pw_MustChange = UtenteDellaLista.Pw_MustChange,
                SecurityStamp = UtenteDellaLista.SecurityStamp,
                ServicePartner = UtenteDellaLista.ServicePartner
            };
            ShowEditDialog = true;
        }

        protected async Task SendMail(string username)
        {
            UtenteSelected = SpSelected.Utenti.Single(x => x.UserName == username);

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
            UtenteSelected = ListaUtentiFiltered.Single(x => x.UserName == username);

            bool isConfirmed = false;
            if (UtenteSelected.IsAbilitato) 
                isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaAbilitazione"]} {localizer["Utente"]}: {UtenteSelected.UserName}", " ");
            else 
                isConfirmed = await Dialogs.ConfirmAsync($"{localizer["ConfermaDisabilitazione"]} {localizer["Utente"]}: {UtenteSelected.UserName}", " ");

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
                //	//false XOR true = true
                UtenteSelected.IsAbilitato ^= true;
            }
        }
    }
}