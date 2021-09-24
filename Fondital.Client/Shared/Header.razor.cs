using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class Header
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthStateTask { get; set; }
        bool ViewUserMenu = false;
        public List<MenuItem> MenuItems { get; set; }
        protected UtenteDto UtenteCorrente { get; set; }

        public class MenuItem
        {
            public string Section { get; set; }
            public string Page { get; set; }
            public List<MenuItem> SubSectionList { get; set; }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthStateTask;
                if (authState.User.Identity.IsAuthenticated)
                    UtenteCorrente = await utClient.GetUtente(authState.User.Identity.Name);

                MenuItems = new List<MenuItem>()
            {
            new MenuItem()
                {
                Section = "",
                SubSectionList = new List<MenuItem>()
                    {
                    new MenuItem()
                        {
                        Section = @localizer["Impostazioni"],
                        Page = "/profile"
                        },
                    new MenuItem()
                        {
                        Section = @localizer["Esci"],
                        Page = "/account/logout"
                        }
                    }
                }
            };

                await base.OnInitializedAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Eccezione header.razor");
            }
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