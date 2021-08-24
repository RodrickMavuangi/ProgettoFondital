using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Telerik.Blazor.Components;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Fondital.Client.Pages
{
	public class ListServicePartnerBase : ComponentBase
	{
		public List<AnagraficaServicePartner> anagraficaServicePartners = new List<AnagraficaServicePartner>();
		public AnagraficaServicePartner AnagraficaServicePartnerModel { get; set; } = new AnagraficaServicePartner();
		public bool WindowVisible { get; set; }
		public bool ValidSubmit { get; set; } = false;
		public EditContext myEditContext { get; set; }

		public List<string> SearchableFields = new List<string> { "RagioneSociale" };


		protected override async Task OnInitializedAsync()
		{
			myEditContext = new EditContext(AnagraficaServicePartnerModel);
			await GetGridData();
		}

		public void EditHandler(GridCommandEventArgs args)
		{
			AnagraficaServicePartner item = (AnagraficaServicePartner)args.Item;

			// TODO Logic ....

		}

		public async Task UpdateHandler(GridCommandEventArgs args)
		{
			AnagraficaServicePartner item = (AnagraficaServicePartner)args.Item;

			//await MyService.Update(item);

			// TODO Logic.....
			await GetGridData();

		}

		public async Task DeleteHandler(GridCommandEventArgs args)
		{
			AnagraficaServicePartner item = (AnagraficaServicePartner)args.Item;

			//await MyService.Delete(item);

			await GetGridData();

		}

		public async Task CreateHandler(GridCommandEventArgs args)
		{
			AnagraficaServicePartner item = (AnagraficaServicePartner)args.Item;

			//await MyService.Create(item);

			await GetGridData();

		}

		public async Task CancelHandler(GridCommandEventArgs args)
		{
			AnagraficaServicePartner item = (AnagraficaServicePartner)args.Item;

			// if necessary, perform actual data source operation here through your service

		}

		public async Task GetGridData()
		{
			//MyData = await MyService.Read();
			for (int i = 0; i < 50; i++)
			{
				anagraficaServicePartners.Add(new AnagraficaServicePartner()
				{
					ID = Guid.NewGuid().ToString(),
					CodiceFornitore = "Fornitore123456" + i.ToString(),
					RagioneSociale = "TechSTORE" + i.ToString(),
					CodiceCliente = "GD654987" + i.ToString(),
					NumeroUtenti = 16 + i
				});
			}
		}




		public void OnSubmitHandler(EditContext editContext)
		{
			bool isFormValid = editContext.Validate();

			if (isFormValid)
			{
				AnagraficaServicePartner anagraficaServicePartnerToSave = (AnagraficaServicePartner)editContext.Model;
				anagraficaServicePartners.Add(anagraficaServicePartnerToSave);

			}
			else
			{
				//apply some custom logic when the form is not valid
			}
		}
	}
}
