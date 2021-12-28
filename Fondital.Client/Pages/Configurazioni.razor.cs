using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Enums;
using Fondital.Shared.Extensions;
using Fondital.Shared.Models;

namespace Fondital.Client.Pages
{
    public partial class Configurazioni
	{
        protected bool isSubmitting = false;
        protected bool? submitSucceded = null;
        protected List<Configurazione> ListaConfigurazioni;

        protected InputModel Input { get; set; } = new();

        protected class InputModel
        {
            [Required]
            public DurataValiditaConfigurazione Garanzia { get; set; }

            [Required]
            public DurataValiditaConfigurazione Password { get; set; }
        }

        protected override async Task OnInitializedAsync()
        {
            ListaConfigurazioni = (List<Configurazione>)await confClient.GetAllConfigurazioni();

            Input.Garanzia = ListaConfigurazioni.SingleOrDefault(x => x.Chiave.ToLower() == "duratagaranzia").Valore.ToEnum<DurataValiditaConfigurazione>();
            Input.Password = ListaConfigurazioni.SingleOrDefault(x => x.Chiave.ToLower() == "duratapassword").Valore.ToEnum<DurataValiditaConfigurazione>();
        }

        async Task SalvaConfigurazioni()
        {
            isSubmitting = true;

            Configurazione ConfigurazioneGaranzia = new() { Chiave = "DurataGaranzia", Valore = Input.Garanzia.ToString() };
            Configurazione ConfigurazionePassword = new() { Chiave = "DurataPassword", Valore = Input.Password.ToString() };

            try
            {
                await confClient.UpdateConfigurazione(ConfigurazioneGaranzia);
                await confClient.UpdateConfigurazione(ConfigurazionePassword);
                isSubmitting = false;
                submitSucceded = true;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                submitSucceded = false;
                throw;
            }
        }

        protected void ResetSubmitTrigger()
        {
            submitSucceded = null;
        }
    }
}