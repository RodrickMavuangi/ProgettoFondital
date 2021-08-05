using Fondital.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Pages
{
    public partial class Traces
    {
        private List<Trace> traces;

        protected override async Task OnInitializedAsync()
        {
            await RefreshTraces();
        }

        protected async void chiamaCreazioneTrace()
        {
            await traceHttp.CreateDummyTrace($"Trace del giorno {DateTime.Now.ToShortDateString()} alle ore {DateTime.Now.ToShortTimeString()}");
            await RefreshTraces();
        }

        protected async Task RefreshTraces()
        {
            traces = (List<Trace>)await traceHttp.GetTraces();
            StateHasChanged();
        }
    }
}
