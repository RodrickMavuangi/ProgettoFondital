using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Enums;
using Microsoft.JSInterop;
using System.Globalization;
using System.Linq;

namespace Fondital.Client.Shared
{
    public partial class Header
    {
        List<string> UserMenuList;
        bool ViewUserMenu = false;

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

        protected override Task OnInitializedAsync()
        {
            UserMenuList = new List<string>
            {
                localizer["CambiaPassword"], localizer["Esci"]
            };

            ViewUserMenu = false;

            var region = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            _currentLang = region.TwoLetterISORegionName;

            return base.OnInitializedAsync();
        }

        public void ToggleUserMenu()
        {
            ViewUserMenu = !ViewUserMenu;
        }

        public void NavigateAnagrafica()
        {
            string subpath = "";

            //non è possibile usare uno switch poiché accetta solo valori statici
            if (CurrentAnag == localizer["AnagraficheSP"])
                subpath = "servicepartners";
            else if (CurrentAnag == localizer["AnagraficheDifetti"])
                subpath = "difetti";
            else if (CurrentAnag == localizer["AnagraficheLavorazioni"])
                subpath = "lavorazioni";
            else if (CurrentAnag == localizer["AnagraficheVociDiCosto"])
                subpath = "vocicosto";
            else if (CurrentAnag == localizer["Listino"])
                subpath = "listino";

            Navigate(subpath);
        }

        public void Navigate(string subpath)
        {
            NavigationManager.NavigateTo($"/{subpath}");
        }
    }
}