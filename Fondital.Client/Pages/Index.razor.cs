using Fondital.Shared.Dto;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class Index
    {
        private UtenteDto UtenteCorrente { get; set; }

        protected async override Task OnInitializedAsync()
        {
            UtenteCorrente = await StateProvider.GetCurrentUser();
        }
    }
}