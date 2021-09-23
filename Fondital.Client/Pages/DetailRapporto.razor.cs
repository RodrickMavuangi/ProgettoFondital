using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class DetailRapporto
    {
        private RapportoDto Rapporto { get; set; } = new RapportoDto() { Stato = 0 };
        private int CurrentStepIndex { get; set; }

        [Parameter] public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id == null)
                await HttpClient.CreateRapporto(Rapporto);
        }

        protected void PreviousStep()
        {
            if (CurrentStepIndex > 0)
                CurrentStepIndex -= 1;
        }

        protected void NextStep()
        {
            if (CurrentStepIndex < 3)
                CurrentStepIndex += 1;
        }
    }
}
