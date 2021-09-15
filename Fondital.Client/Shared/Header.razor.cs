using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Enums;
using Microsoft.JSInterop;
using System.Globalization;
using System.Linq;
using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Fondital.Shared.Dto;

namespace Fondital.Client.Shared
{
    public partial class Header
    {
        [CascadingParameter]
        private Task<AuthenticationState> authStateTask { get; set; }

        List<string> UserMenuList;
        bool ViewUserMenu = false;
        public List<MenuItem> MenuItems { get; set; }
        protected UtenteDto UtenteCorrente { get; set; }

        public class MenuItem
        {
            public string Section { get; set; }
            public string Page { get; set; }
            public List<MenuItem> SubSectionList { get; set; }
        }

        public static IEnumerable<string> SupportedLanguages { get => EnumExtensions.GetEnumNames<Lingua>(); }
        private string _currentLang { get; set; }

        public string CurrentLang
        {
            get => _currentLang;
            set
            {
                _currentLang = value;

                var curLang = EnumExtensions.GetEnumValues<Lingua>().FirstOrDefault(l => l.ToString() == value);

                if (CultureInfo.CurrentCulture.Name != curLang.Description())
                {
                    var js = (IJSInProcessRuntime)JS;
                    js.InvokeVoid("blazorCulture.set", curLang.Description());

                    NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authStateTask;
            if (authState.User.Identity.IsAuthenticated)
                UtenteCorrente = await utClient.GetUtente(authState.User.Identity.Name);

            UserMenuList = new List<string>
            {
                localizer["CambiaPassword"], localizer["Esci"]
            };

            ViewUserMenu = false;

            var region = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            _currentLang = region.TwoLetterISORegionName;

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