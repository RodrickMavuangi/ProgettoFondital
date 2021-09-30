using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class LoginDisplay
    {
        async Task BeginSignOut()
        {
            await SignOutManager.SetSignOutState();

            Navigation.NavigateTo("account/logout");
        }
    }
}