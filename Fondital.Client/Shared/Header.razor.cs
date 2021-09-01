using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Enums;

namespace Fondital.Client.Shared
{
    public partial class Header
    {
        string CurrentAnag = string.Empty;
        List<Lingua> Langs;
        List<string> Anags;
        List<string> UserMenuList;
        bool ViewUserMenu = false;

        protected override Task OnInitializedAsync()
        {
            Anags = new List<string>
            {
                localizer["AnagraficheSP"], localizer["AnagraficheDifetti"], localizer["AnagraficheLavorazioni"], localizer["AnagraficheVociDiCosto"], localizer["Listino"]
            };

            CurrentAnag = null;
            UserMenuList = new List<string>
            {
                localizer["CambiaPassword"], localizer["Esci"]
            };

            ViewUserMenu = false;

            return base.OnInitializedAsync();
        }

        public void ToggleUserMenu()
        {
            ViewUserMenu = !ViewUserMenu;
        }

        public void Navigate(string subpath)
        {
            NavigationManager.NavigateTo($"/{subpath}");
        }
    }
}
