using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.AuthPages
{
    [AllowAnonymous]
    public partial class Login
    {
        LoginRequestDto Model { get; set; } = new();
        LoginResponseDto LoginResponse = new();
        protected string Error { get; set; }

        public async Task UserLogin()
        {
            try
            {
                LoginResponse = await authClient.Login(Model);
                await loginService.Login(LoginResponse.Token);
                navManager.NavigateTo("");
            }
            catch (Exception e)
            {
                if (e.Message == "PasswordMustChange")
                    navManager.NavigateTo($"/account/changepassword/{Model.Email}");
                else
                    Error = e.Message;
            }
        }
    }
}