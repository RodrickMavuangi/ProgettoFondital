using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Client.Pages
{
	public partial class Configurazioni
	{
        protected bool isSubmitting = false;
        protected bool? submitSucceded = null;

        protected InputModel Input { get; set; } = new InputModel();

        protected class InputModel
        {
            [Required]
            public DurataValidita Garanzia { get; set; }

            [Required]
            public DurataValidita Password { get; set; }
        }

        protected override async Task OnInitializedAsync()
        {
            List<Configurazione> Configurazioni = (List<Configurazione>)await confClient.GetAllConfigurazioni();

            Input.Garanzia = Configurazioni.SingleOrDefault(x => x.Chiave.ToLower() == "duratagaranzia").Valore.ToEnum<DurataValidita>();
            Input.Password = Configurazioni.SingleOrDefault(x => x.Chiave.ToLower() == "duratapassword").Valore.ToEnum<DurataValidita>();
        }

        async Task SalvaConfigurazioni()
        {
            isSubmitting = true;

            Configurazione ConfigurazioneGaranzia = new Configurazione { Chiave = "DurataGaranzia", Valore = Input.Garanzia.ToString() };
            Configurazione ConfigurazionePassword = new Configurazione { Chiave = "DurataPassword", Valore = Input.Password.ToString() };

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
