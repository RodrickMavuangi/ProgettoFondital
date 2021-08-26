using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class Header
    {
        string UserFirstName = string.Empty;
        string UserLastName = string.Empty;
        string CurrentLanguage = string.Empty;
        List<string> Languages;
        string CurrentAnag = string.Empty;
        List<string> Anags;
        List<string> UserMenuList;
        bool ViewUserMenu = false;

        protected override Task OnInitializedAsync()
        {
            Languages = new List<string>
            {
                "ITA", "RUS"
            };

            CurrentLanguage = "ITA";
            Anags = new List<string>
                {
                    "Anagrafiche SP", "Anagrafiche Difetti", "Anagrafiche Lavorazioni", "Anagrafiche Voci di Costo", "Listino"
                };

            CurrentAnag = null;
            UserMenuList = new List<string>
                    {
                        "Cambia Password", "LogOut"
                        };

            ViewUserMenu = false;
            UserFirstName = "Lupo";
            UserLastName = "Lucio";

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
