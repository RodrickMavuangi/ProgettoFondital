using Fondital.Client.Shared;
using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.AuthPages
{
    [AllowAnonymous]
    public partial class Login
    {
        [CascadingParameter]
        protected Header Header { get; set; }

        LoginRequestDto Model { get; set; } = new();
        LoginResponseDto LoginResponse = new();
        protected string Error { get; set; }
        protected bool IsSubmitting = false;

        public async Task UserLogin()
        {
            try
            {
                IsSubmitting = true;
                LoginResponse = await authClient.Login(Model);
                await loginService.Login(LoginResponse.Token);
                StateHasChanged();
                await Header.PopolaUtente();
                navManager.NavigateTo("");
            }
            catch (Exception e)
            {
                IsSubmitting = false;
                if (e.Message == "PasswordMustChange")
                    navManager.NavigateTo($"/account/changepassword/{Model.Email}");
                else
                    Error = e.Message;
            }
        }
    }
}