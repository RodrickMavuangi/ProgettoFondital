using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Enums;

namespace Fondital.Client.Shared
{
    public partial class Header
    {
        string UserFirstName = string.Empty;
        string UserLastName = string.Empty;
        
        string CurrentAnag = string.Empty;
        List<Lingua> Langs;
        List<string> Anags;
        List<string> UserMenuList;
        bool ViewUserMenu = false;

        protected override Task OnInitializedAsync()
        {
            Langs = Enum.GetValues(typeof(Lingua)).Cast<Lingua>().ToList();
            
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
            UserFirstName = authState.UtenteCorrente?.Nome ?? "Lupo";
            UserLastName = authState.UtenteCorrente?.Cognome ?? "Lucio";

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
