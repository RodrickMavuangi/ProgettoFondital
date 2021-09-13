using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class Logout
    {
        protected async override Task OnInitializedAsync()
        {
            await loginService.Logout();
            navigationManager.NavigateTo("");
        }
    }
}
