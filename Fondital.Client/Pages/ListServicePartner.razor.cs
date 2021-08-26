using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Telerik.Blazor.Components;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using Fondital.Shared.Services;
using Fondital.Shared;
using Fondital.Services;
using Fondital.Shared.Models.Auth;
using Fondital.Client.Clients;

namespace Fondital.Client.Pages
{
	public class ListServicePartnerBase : ComponentBase
	{
		public List<ServicePartner> ServicePartners = new List<ServicePartner>();
		public ServicePartner ServicePartnerModel { get; set; } = new ServicePartner();
		public bool WindowVisible { get; set; }
		public bool ValidSubmit { get; set; } = false;
		public EditContext myEditContext { get; set; }

		public List<string> SearchableFields = new List<string> { "RagioneSociale" };
		[Inject] public ServicePartnerClient servicePartnerClient { get; set; }
		protected override async Task OnInitializedAsync()
		{
			myEditContext = new EditContext(ServicePartnerModel);
			ServicePartners = (List<ServicePartner>)await servicePartnerClient.GetAllServicePartners();
		}

        public async Task EditHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;
			//TODO Logic ....
		}

		public async Task UpdateHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;
			await servicePartnerClient.UpdateServicePartner(item.Id,item);
			await Refresh();
			
			// TODO Logic.....

		}

		public async Task DeleteHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;

		}

		public async Task CreateHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;

		}

		public async Task CancelHandler(GridCommandEventArgs args)
		{
			ServicePartner item = (ServicePartner)args.Item;

			// if necessary, perform actual data source operation here through your service

		}

		async Task Refresh() 
		{
			WindowVisible = false;
			await OnInitializedAsync();
		}

		public async Task OnSubmitHandlerAsync(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();

			if (isFormValid)
			{
				ServicePartner ServicePartnerToSave = (ServicePartner)editContext.Model;

				//ServicePartnerToSave.Id = Int32.Parse(Guid.NewGuid().ToString());

				//Utente utente1 = new Utente() {
				//	ServicePartner = ServicePartnerToSave,
				//	IsAbilitato = true,
				//	Cognome = "Utente1",
				//	Pw_LastChanged = new DateTime(2021, 08, 24),
				//	Pw_MustChange = false
				//};

				//Utente utente2 = new Utente()
				//{
				//	ServicePartner = ServicePartnerToSave,
				//	IsAbilitato = false,
				//	Cognome = "Utente2",
				//	Pw_LastChanged = new DateTime(2021, 01, 20),
				//	Pw_MustChange = false
				//};
				//anagraficaServicePartners.Add(anagraficaServicePartnerToSave);

				//ServicePartnerToSave.Utenti = new List<Utente>() { utente1, utente2 };
				await servicePartnerClient.CreateServicePartner(ServicePartnerToSave);
				await Refresh();
			}
			else
			{
				//apply some custom logic when the form is not valid
			}
		}
	}
}
