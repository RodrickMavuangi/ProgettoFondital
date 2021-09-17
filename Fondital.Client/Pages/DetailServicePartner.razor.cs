using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;


namespace Fondital.Client.Pages
{
    public partial class DetailServicePartner
	{
		public string SearchText = "";	
		public StatoUtente ConStato { get; set; } = new StatoUtente();
		[CascadingParameter]
		public DialogFactory Dialogs { get; set; }
		UtenteDto DatiUtente = new UtenteDto();
		public List<string> ListaScelta { get; set; } = new List<string>() { };
		public string SceltaCorrente = string.Empty;
		[Parameter]
		public string servicePId { get; set; }
		private List<UtenteDto> ListaUtenti = new List<UtenteDto>();
		protected bool ShowAddDialog { get; set; } = false;
		protected bool ShowEditDialog { get; set; } = false;
		protected UtenteDto UtenteSelected { get; set; }
		protected ServicePartnerDto SpSelected { get; set; } = new ServicePartnerDto() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };

		protected override async Task OnInitializedAsync()
		{
			ListaScelta = new List<string>() { @localizer["Tutti"], @localizer["Abilitati"], @localizer["Disabilitati"] };
			SpSelected = await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));

			if (SpSelected.Utenti == null) SpSelected.Utenti = new List<UtenteDto>();

			SceltaCorrente = null;
			await RefreshUtenti();
		}

		public List<UtenteDto> ListaUtenti_Filtered => ConStato.Abilitati == true ? ListaUtenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower()) && x.IsAbilitato == true).ToList() :
													   ConStato.Disabilitati == true ? ListaUtenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower()) && x.IsAbilitato == false).ToList() :
													   ListaUtenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower())).ToList();

		protected async Task CloseAndRefresh()
		{
			ShowAddDialog = false;
			ShowEditDialog = false;
			await RefreshUtenti();
		}

		protected async Task RefreshUtenti()
		{
			ListaUtenti = (List<UtenteDto>) await utenteClient.GetUtenti();
			StateHasChanged();
		}

		protected void EditUtente(int utenteId)
		{
			UtenteSelected = ListaUtenti.Single(x => x.Id == utenteId);
			ShowEditDialog = true;
		}

		protected async Task EditSp(int SpId)
		{
			SpSelected = await  servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));
			ShowEditDialog = true;
		}


		protected async Task sendMail(int utenteId)
		{
			UtenteSelected = ListaUtenti.Single(x => x.Id == utenteId);
			UtenteDto UtenteToSendMail = (UtenteDto)await utenteClient.GetUtente(UtenteSelected.UserName);
			bool isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["InviaMail"]} {UtenteToSendMail.Nome} {UtenteToSendMail.Cognome} {localizer["ResetPassword"]}");
			if (isConfirmed)
			{
				MailRequest mailRequest = new MailRequest()
				{
					ToEmail = UtenteToSendMail.UserName,
					Subject = "Risetta la Password",
				};

				await mailClient.sendMail(mailRequest);
				await Dialogs.AlertAsync($"{@localizer["MailInviata"]} {UtenteToSendMail.Email} {@localizer["ResetPassword"]}");
			}
		}


		protected async Task UpdateEnableUtente(int Id)
		{
			UtenteDto ut = ListaUtenti_Filtered.Single(x => x.Id == Id);
			bool isConfirmed = false;
			if (ut.IsAbilitato) isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ConfermaModificaUtenteAb"]} {ut.Nome} {ut.Cognome} ?", @localizer["ModificaUtente"]);
			else isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ConfermaModificaUtente"]} {ut.Nome} {ut.Cognome} ?", localizer["ModificaUtente"]);

			if (isConfirmed)
			{
				try
				{
					await utenteClient.UpdateUtente(Id, ut);
					await CloseAndRefresh();
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
				ListaUtenti_Filtered.Single(x => x.Id == Id).IsAbilitato ^= true;
			}
		}


		public async Task MyValueChangeHandler(string theUserChoice)
		{
			switch (theUserChoice)
			{
				case "Tutti":
					ConStato = new StatoUtente() { Tutti = true, Abilitati = false, Disabilitati = false };
					break;
				case "Abilitati":
					ConStato = new StatoUtente() { Tutti = false, Abilitati = true, Disabilitati = false };
					break;
				case "Disabilitati":
					ConStato = new StatoUtente() { Tutti = false, Abilitati = false, Disabilitati = true };
					break;
			}
		}


		public class StatoUtente
		{

			public bool Tutti { get; set; } = false;
			public bool Abilitati { get; set; } = false;
			public bool Disabilitati { get; set; } = false;

		}


	}
}