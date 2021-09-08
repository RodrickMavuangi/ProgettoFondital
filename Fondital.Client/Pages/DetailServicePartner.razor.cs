//using BlastOff.Shared;
using Fondital.Client.Clients;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Fondital.Client.ClientModel;
using Microsoft.Extensions.Localization;

namespace Fondital.Client.Pages
{
	public class DetailServicePartnerBase : ComponentBase
	{
		public List<Utente> ListUtenti { get; set; } = new List<Utente>();
		public string SearchText = "";
		public StatoUtente ConStato { get; set; } = new StatoUtente();
		[CascadingParameter]
		public DialogFactory Dialogs { get; set; }
		public bool WindowVisible { get; set; }
		public bool isModalVisible { get; set; } = false;
		public bool ValidSubmit { get; set; } = false;
		public bool myEditTemplate { get; set; } = false;
		public Utente ServicePartnerModel_AddUtente { get; set; } = new Utente();
		public ServicePartner ServicePartnerModel_UpdateSP { get; set; } = new ServicePartner() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };
		public Utente UtenteModel_EditUtente { get; set; } = new Utente { Email = "", Nome = "", Cognome = "" };
		public EditContext myEditContext_AddUTente { get; set; } 
		public EditContext myEditContext_UpdateSP { get; set; }
		public EditContext myEditContext_UpdateUtente { get; set; }
		[Inject] public ServicePartnerClient servicePartnerClient { get; set; }
		[Inject] public UtenteClient utenteClient { get;set; }
		[Inject] public MailClient mailClient { get; set; }
		[Inject] private IStringLocalizer<App> localizer { get; set; }
		public int UtentiAbilitati { get; set; } = 0;
		public int UtentiDisabilitati { get; set; } = 0;
		public ServicePartner servicePartnersWithUtenti { get; set; } = new ServicePartner() { Utenti = new List<Utente>()};
		Utente DatiUtente = new Utente();
		public List<string> ListaScelta { get; set; } = new List<string>() { };
		public string SceltaCorrente = string.Empty;
	    [Parameter]
		public string servicePId { get; set; }



		protected override async Task OnInitializedAsync()
		{
			ListaScelta = new List<string>() { @localizer["Tutti"], localizer["Abilitati"], localizer["Disabilitati"] };
			myEditContext_AddUTente = new EditContext(ServicePartnerModel_AddUtente);
			myEditContext_UpdateSP = new EditContext(ServicePartnerModel_UpdateSP);
			myEditContext_UpdateUtente = new EditContext(UtenteModel_EditUtente);

			ServicePartnerModel_UpdateSP = await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));
			ListUtenti = ServicePartnerModel_UpdateSP.Utenti;
			if (ListUtenti == null){
				ListUtenti = new List<Utente>();
				ServicePartnerModel_UpdateSP.Utenti = ListUtenti;

			}
			UtentiAbilitati = ServicePartnerModel_UpdateSP.Utenti.Where(x => x.IsAbilitato == true).Count();
			UtentiDisabilitati = ServicePartnerModel_UpdateSP.Utenti.Where(x => x.IsAbilitato == false).Count();

			servicePartnersWithUtenti =(ServicePartner)await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));

			SceltaCorrente = null;
		}


		public List<Utente> FilteredUtenti => servicePartnersWithUtenti.Utenti.Where<Utente>(x => x.Email.Contains(SearchText)).ToList();
		public List<Utente> FilterdUtenti_Abilitati => servicePartnersWithUtenti.Utenti.Where<Utente>(x => x.Email.Contains(SearchText) && x.IsAbilitato == true).ToList();
		public List<Utente> FilterdUtenti_Disabilitati => servicePartnersWithUtenti.Utenti.Where<Utente>(x => x.Email.Contains(SearchText) && x.IsAbilitato == false).ToList();

		public async Task OnSubmitHandlerAsync(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();

			if (isFormValid)
			{
				Utente UtenteToSave = (Utente)editContext.Model;
				UtenteToSave.IsAbilitato = false;
				UtenteToSave.Email = UtenteToSave.UserName;
				if (ServicePartnerModel_UpdateSP.Utenti == null) ServicePartnerModel_UpdateSP.Utenti = new List<Utente>();
				ServicePartnerModel_UpdateSP.Utenti.Add(UtenteToSave);
				await servicePartnerClient.UpdateServicePartner(ServicePartnerModel_UpdateSP.Id, ServicePartnerModel_UpdateSP);
				ServicePartnerModel_AddUtente = new Utente();
				await Refresh();

			}
			else
			{
				//apply some custom logic when the form is not valid
			}
		}
		public async Task OnSubmit_updateService_HandlerAsync(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();
			if (isFormValid)
			{
				ServicePartner ServicePartnerToSave = (ServicePartner)editContext.Model;

				await servicePartnerClient.UpdateServicePartner(ServicePartnerToSave.Id, ServicePartnerToSave);
				await Refresh();
			}
			else
			{
				
			}
		}
		async Task Refresh()
		{
			WindowVisible = false;
			isModalVisible = false;
			myEditTemplate = false;
			await OnInitializedAsync();
		}

		public async Task EditHandler(GridCommandEventArgs args)
		{
			myEditTemplate = true;
			DatiUtente = (Utente)args.Item;
			UtenteModel_EditUtente.Cognome = DatiUtente.Cognome;
			UtenteModel_EditUtente.Nome = DatiUtente.Nome;
			UtenteModel_EditUtente.Email = DatiUtente.Email;
		}

		public async Task EditUtente_UpdateHandler(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();
			if (isFormValid)
			{
				Utente utente = (Utente)editContext.Model;
				utente.IsAbilitato = DatiUtente.IsAbilitato;
				await utenteClient.UpdateUtente(DatiUtente.Id, utente);
				await Refresh();
				DatiUtente = new Utente();
			}
			else
			{

			}
		}

		protected async Task UpdateEnableUtente(int Id)
		{
			Utente ut = FilteredUtenti.Single(x => x.Id == Id);
			bool isConfirmed = false;
			if (ut.IsAbilitato)isConfirmed = await Dialogs.ConfirmAsync(localizer[$"Si è sicuri di voler abilitare l'utente {ut.Nome} {ut.Cognome} ?", "Modifica utente"]);
			else isConfirmed = await Dialogs.ConfirmAsync(localizer[$"Si è sicuri di voler disabilitare l'utente {ut.Nome} {ut.Cognome} ?", "Modifica utente"]);
			
			if (isConfirmed)
			{
				try
				{
					await utenteClient.UpdateUtente(Id, ut) ;
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
				FilteredUtenti.Single(x => x.Id == Id).IsAbilitato ^= true;
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
			Utente utente = (Utente)args.Item;
			bool isConfirmed = await Dialogs.ConfirmAsync(localizer[$"Vuoi mandare una mail all'utente {utente.Nome} {utente.Cognome} per il reset della password ? "]);
			if (isConfirmed)
			{
				MailRequest mailRequest = new MailRequest()
				{
					ToEmail = "mavelec@libero.it",
					Subject = "Hello world",
					Body = "<h1>Hello world<h1>"
				};

				await mailClient.sendMail(mailRequest);
			}
			
		} 

		
	}
}