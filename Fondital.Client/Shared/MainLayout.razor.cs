using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class MainLayout
    {
        protected Header Header { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeVoidAsync("manipulateActiveLink",
                firstRender,
                FonditalNavigationManager.ToBaseRelativePath(FonditalNavigationManager.Uri));
        }
    }
}