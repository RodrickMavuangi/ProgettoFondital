using Fondital.Client.ClientModel;
using Fondital.Client.Clients;
using Fondital.Shared.Dto;
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
    public class DetailServicePartnerBase : ComponentBase
	{
		public List<UtenteDto> ListUtenti { get; set; } = new List<UtenteDto>();
		public string SearchText = "";
		public StatoUtente ConStato { get; set; } = new StatoUtente();
		[CascadingParameter]
		public DialogFactory Dialogs { get; set; }
		public bool WindowVisible { get; set; }
		public bool isModalVisible { get; set; } = false;
		public bool ValidSubmit { get; set; } = false;
		public bool myEditTemplate { get; set; } = false;
		public UtenteDto ServicePartnerModel_AddUtente { get; set; } = new UtenteDto();
		public ServicePartnerDto ServicePartnerModel_UpdateSP { get; set; } = new ServicePartnerDto() { CodiceCliente = "", CodiceFornitore = "", RagioneSociale = "" };
		public UtenteDto UtenteModel_EditUtente { get; set; } = new UtenteDto { Email = "", Nome = "", Cognome = "" };
		public EditContext myEditContext_AddUTente { get; set; } 
		public EditContext myEditContext_UpdateSP { get; set; }
		public EditContext myEditContext_UpdateUtente { get; set; }
		[Inject] public ServicePartnerClient servicePartnerClient { get; set; }
		[Inject] public UtenteClient utenteClient { get;set; }
		[Inject] public MailClient mailClient { get; set; }
		[Inject] private IStringLocalizer<App> localizer { get; set; }
		public int UtentiAbilitati { get; set; } = 0;
		public int UtentiDisabilitati { get; set; } = 0;
		public ServicePartnerDto servicePartnersWithUtenti { get; set; } = new ServicePartnerDto() { Utenti = new List<UtenteDto>()};

		UtenteDto DatiUtente = new UtenteDto();
		public List<string> ListaScelta { get; set; } = new List<string>() { };

		public string SceltaCorrente = string.Empty;

	    [Parameter]
		public string servicePId { get; set; }

		protected ServicePartnerDto SpSelected { get; set; } = new ServicePartnerDto();
		protected bool ShowEditSpDialog { get; set; } = false;
		protected bool ShowEditUserDialog { get; set; } = false;



		protected override async Task OnInitializedAsync()
		{
			ListaScelta = new List<string>() { @localizer["Tutti"], @localizer["Abilitati"], @localizer["Disabilitati"] };
			myEditContext_AddUTente = new EditContext(ServicePartnerModel_AddUtente);
			myEditContext_UpdateSP = new EditContext(ServicePartnerModel_UpdateSP);
			myEditContext_UpdateUtente = new EditContext(UtenteModel_EditUtente);

			ServicePartnerModel_UpdateSP = await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));
			ListUtenti = ServicePartnerModel_UpdateSP.Utenti;
			if (ListUtenti == null){
				ListUtenti = new List<UtenteDto>();
				ServicePartnerModel_UpdateSP.Utenti = ListUtenti;

			}
			UtentiAbilitati = ServicePartnerModel_UpdateSP.Utenti.Where(x => x.IsAbilitato == true).Count();
			UtentiDisabilitati = ServicePartnerModel_UpdateSP.Utenti.Where(x => x.IsAbilitato == false).Count();

			servicePartnersWithUtenti =(ServicePartnerDto)await servicePartnerClient.GetServicePartnerWithUtenti(int.Parse(servicePId));

			SceltaCorrente = null;
		}


		public List<UtenteDto> FilteredUtenti => servicePartnersWithUtenti.Utenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower())).ToList();
		public List<UtenteDto> FilterdUtenti_Abilitati => servicePartnersWithUtenti.Utenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower()) && x.IsAbilitato == true).ToList();
		public List<UtenteDto> FilterdUtenti_Disabilitati => servicePartnersWithUtenti.Utenti.Where<UtenteDto>(x => x.Email.ToLower().Contains(SearchText.ToLower()) && x.IsAbilitato == false).ToList();


		protected async Task CloseAndRefresh()
        {
			ShowEditSpDialog = false;
			await Refresh();
        }

		protected async Task EditSp()
		{
			SpSelected = await servicePartnerClient.GetServicePartnerById(int.Parse(servicePId));
			ShowEditSpDialog = true;
		}


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
				await servicePartnerClient.UpdateServicePartner(ServicePartnerModel_UpdateSP.Id, ServicePartnerModel_UpdateSP);
				ServicePartnerModel_AddUtente = new UtenteDto();
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
				ServicePartnerDto ServicePartnerToSave = (ServicePartnerDto)editContext.Model;

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
			DatiUtente = (UtenteDto)args.Item;
			UtenteModel_EditUtente.Cognome = DatiUtente.Cognome;
			UtenteModel_EditUtente.Nome = DatiUtente.Nome;
			UtenteModel_EditUtente.Email = DatiUtente.Email;
		}

		public async Task EditUtente_UpdateHandler(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();
			if (isFormValid)
			{
				UtenteDto utente = (UtenteDto)editContext.Model;
				utente.IsAbilitato = DatiUtente.IsAbilitato;
				await utenteClient.UpdateUtente(DatiUtente.Id, utente);
				await Refresh();
				DatiUtente = new UtenteDto();
			}
			else
			{

			}
		}

		protected async Task UpdateEnableUtente(int Id)
		{
			UtenteDto ut = FilteredUtenti.Single(x => x.Id == Id);
			bool isConfirmed = false;
			if (ut.IsAbilitato)isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ConfermaModificaUtente"]} {ut.Nome} {ut.Cognome} ?",@localizer["ModificaUtente"]);
			else isConfirmed = await Dialogs.ConfirmAsync($"{@localizer["ConfermaModificaUtenteAb"]} {ut.Nome} {ut.Cognome} ?", localizer["ModificaUtente"]);
			
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
			//Utente utente = (Utente)args.Item;
			//bool isConfirmed = await Dialogs.ConfirmAsync(localizer[$"Vuoi mandare una mail all'utente {utente.Nome} {utente.Cognome} per il reset della password ? "]);
			//if (isConfirmed)
			//{
			//	MailRequest mailRequest = new MailRequest()
			//	{
			//		ToEmail = "mavelec@libero.it",
			//		Subject = "Hello world",
			//		Body = "<h1>Hello world<h1>"
			//	};

			//	await mailClient.sendMail(mailRequest);
			//}
			
		} 

		
	}
}