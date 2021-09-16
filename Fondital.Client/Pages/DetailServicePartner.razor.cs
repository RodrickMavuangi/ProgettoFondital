using Fondital.Client.Clients;
using Fondital.Shared.Dto;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;
using Telerik.Blazor.Components;

namespace Fondital.Client.Pages
{
    public partial class DetailServicePartner
	{
		public string SearchText = "";
		public StatoUtente ConStato { get; set; } = new StatoUtente();
		[CascadingParameter]
		public DialogFactory Dialogs { get; set; }
		public bool WindowVisible { get; set; }
		public bool isModalVisible { get; set; } = false;
		public bool ValidSubmit { get; set; } = false;
		public UtenteDto ServicePartnerModel_AddUtente { get; set; } = new UtenteDto();
		public ServicePartnerDto ServicePartnerModel_UpdateSP { get; set; } = new ServicePartnerDto() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };
		public UtenteDto UtenteModel_EditUtente { get; set; } = new UtenteDto { Email = "", Nome = "", Cognome = "" };
		public EditContext myEditContext_AddUTente { get; set; }
		public EditContext myEditContext_UpdateSP { get; set; }
		public EditContext myEditContext_UpdateUtente { get; set; }
		//[Inject] public ServicePartnerClient servicePartnerClient { get; set; }
		//[Inject] public UtenteClient utenteClient { get; set; }
		//[Inject] public MailClient mailClient { get; set; }
		//[Inject] private IStringLocalizer<App> localizer { get; set; }
		public int UtentiAbilitati { get; set; } = 0;
		public int UtentiDisabilitati { get; set; } = 0;
		public ServicePartnerDto servicePartnersWithUtenti { get; set; } = new ServicePartnerDto() { Utenti = new List<UtenteDto>() };

		UtenteDto DatiUtente = new UtenteDto();
		public List<string> ListaScelta { get; set; } = new List<string>() { };

		public string SceltaCorrente = string.Empty;

		[Parameter]
		public string servicePId { get; set; }

		//------------------------------------------------------------------------------
		private List<UtenteDto> ListaUtenti = new List<UtenteDto>();
		protected bool ShowAddDialog { get; set; } = false;
		protected bool ShowEditDialog { get; set; } = false;
		protected UtenteDto UtenteSelected { get; set; }
		//----------------------------------------------------------
		protected override async Task OnInitializedAsync()
		{
			ListaScelta = new List<string>() { @localizer["Tutti"], @localizer["Abilitati"], @localizer["Disabilitati"] };
			myEditContext_AddUTente = new EditContext(ServicePartnerModel_AddUtente);
			myEditContext_UpdateSP = new EditContext(ServicePartnerModel_UpdateSP);
			myEditContext_UpdateUtente = new EditContext(UtenteModel_EditUtente);

			ServicePartnerModel_UpdateSP = await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));

			if (ServicePartnerModel_UpdateSP.Utenti == null) ServicePartnerModel_UpdateSP.Utenti = new List<UtenteDto>();

			UtentiAbilitati = ServicePartnerModel_UpdateSP.Utenti.Where(x => x.IsAbilitato == true).Count();
			UtentiDisabilitati = ServicePartnerModel_UpdateSP.Utenti.Where(x => x.IsAbilitato == false).Count();

			servicePartnersWithUtenti = (ServicePartnerDto)await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));

			SceltaCorrente = null;
			//------------
			await RefreshUtenti();
			//-----------
		}
		public List<UtenteDto> ListaUtenti_Filtered => ConStato.Abilitati == true ? servicePartnersWithUtenti.Utenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower()) && x.IsAbilitato == true).ToList() :
													   ConStato.Disabilitati == true ? servicePartnersWithUtenti.Utenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower()) && x.IsAbilitato == false).ToList() :
													   servicePartnersWithUtenti.Utenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower())).ToList();

		public async Task OnSubmitHandlerAsync(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();

			if (isFormValid)
			{
				UtenteDto UtenteToSave = (UtenteDto)editContext.Model;
				UtenteToSave.IsAbilitato = false;  
				UtenteToSave.Email = UtenteToSave.UserName;

				if (ServicePartnerModel_UpdateSP.Utenti == null) ServicePartnerModel_UpdateSP.Utenti = new List<UtenteDto>();
				ServicePartnerModel_UpdateSP.Utenti.Add(UtenteToSave);
				ServicePartnerModel_AddUtente = new UtenteDto();
				await mailClient.sendMailForNewUser(UtenteToSave, ServicePartnerModel_UpdateSP);
				await Refresh();
				//await Dialogs.AlertAsync($"{@localizer["MailInviato"]} {UtenteToSave.Email} {@localizer["PrimaImposaPassword"]}");

			}
		}


		public async Task OnSubmit_updateService_HandlerAsync(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();
			if (isFormValid)
			{
				ServicePartnerDto ServicePartnerToSave = (ServicePartnerDto)editContext.Model;

				await servicePartnerClient.UpdateServicePartner(ServicePartnerToSave.Id, ServicePartnerToSave);
				await Refresh();
			}
		}
		//----------------------------------------------------------
		
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

		protected async Task sendMail_b(int utenteId)
		{
			UtenteSelected = ListaUtenti.Single(x => x.Id == utenteId);
			//UtenteDto utente = (UtenteDto)args.Item;
			UtenteDto UtenteToSendMail = (UtenteDto)await utenteClient.GetUtente(UtenteSelected.UserName);
			bool isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["MandaMail"]} {UtenteToSendMail.Nome} {UtenteToSendMail.Cognome} {localizer["ResetPassword"]}");
			if (isConfirmed)
			{
				MailRequest mailRequest = new MailRequest()
				{
					ToEmail = UtenteToSendMail.UserName,
					Subject = "Crea la Password",
				};

				await mailClient.sendMail(mailRequest);
				await Dialogs.AlertAsync($"{@localizer["MailInviato"]} {UtenteToSendMail.Email} {@localizer["ResetPassword"]}");
			}
		}

		//------------------------------------------------------------------------------------
		async Task Refresh()
		{
			WindowVisible = false;
			isModalVisible = false;
			await OnInitializedAsync();
		}

		//public async Task EditHandler(GridCommandEventArgs args)
		//{
		//	myEditTemplate = true;
		//	DatiUtente = (UtenteDto)args.Item;
		//	UtenteModel_EditUtente.Cognome = DatiUtente.Cognome;
		//	UtenteModel_EditUtente.Nome = DatiUtente.Nome;
		//	UtenteModel_EditUtente.Email = DatiUtente.Email;
		//}

		//public async Task EditUtente_UpdateHandler(EditContext editContext)
		//{
		//	bool isFormValid = editContext.Validate();
		//	if (isFormValid)
		//	{
		//		UtenteDto utente = (UtenteDto)editContext.Model;
		//		utente.IsAbilitato = DatiUtente.IsAbilitato;
		//		await utenteClient.UpdateUtente(DatiUtente.Id, utente);
		//		await Refresh();
		//		DatiUtente = new UtenteDto();
		//	}
		//}

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
					await Refresh();
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

		public async Task SendMail(GridCommandEventArgs args)
		{
			UtenteDto utente = (UtenteDto)args.Item;
			UtenteDto UtenteToSendMail = (UtenteDto) await  utenteClient.GetUtente(utente.UserName);
			bool isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["MandaMail"]} {utente.Nome} {utente.Cognome} {localizer["ResetPassword"]}");
			if (isConfirmed)
			{
				MailRequest mailRequest = new MailRequest()
				{
					ToEmail = UtenteToSendMail.UserName,
					Subject = "Crea la Password",
				};

				await mailClient.sendMail(mailRequest);
				await Dialogs.AlertAsync($"{@localizer["MailInviato"]} {utente.Email} {@localizer["ResetPassword"]}");
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