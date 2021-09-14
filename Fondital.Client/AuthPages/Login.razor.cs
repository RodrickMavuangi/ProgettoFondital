using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Fondital.Shared.Resources;

namespace Fondital.Client.AuthPages
{
    [AllowAnonymous]
    public partial class Login
    {
        LoginRequest model = new LoginRequest();
        LoginResponse loginResponse = new LoginResponse();
        protected string Error { get; set; }

        public async Task UserLogin()
        {
            try
            {
                loginResponse = await authClient.Login(model);
                await loginService.Login(loginResponse.Token);
                navManager.NavigateTo("");
            }
            catch (Exception e)
            {
                if (e.Message == "PasswordMustChange")
                    navManager.NavigateTo($"/account/changepassword/{model.Email}");
                else
                    Error = e.Message;
            }
        }
    }
}