using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class Alert
    {
        private bool IsVisible = false;

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public IEnumerable<string> ErrorList { get; set; }

        [Parameter]
        public string CSSClass { get; set; } = "warning";

        protected override void OnParametersSet()
        {
            IsVisible = !String.IsNullOrEmpty(Title) && (ErrorList != null && ErrorList.Count() > 0);
        }
    }
}
