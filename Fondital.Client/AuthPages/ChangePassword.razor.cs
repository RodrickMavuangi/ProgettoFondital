using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Fondital.Client.AuthPages
{
    public partial class ChangePassword
    {
        [Parameter]
        public string Email { get; set; }
        private LoginResponseDto LoginResponse { get; set; }
        protected ChangePwRequestDto ChangeForm { get; set; } = new ChangePwRequestDto();
        protected string Error { get; set; }
        protected bool IsSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            ChangeForm.Email = Email;
            Error = localizer["PasswordMustChange"];
        }

        protected async Task ChangePw()
        {
            try
            {
                IsSubmitting = true;

                if (ChangeForm.NewPassword == ChangeForm.ConfirmPassword && ChangeForm.OldPassword != ChangeForm.NewPassword)
                {
                    await authClient.ChangePassword(ChangeForm);
                    LoginResponse = await authClient.Login(new LoginRequestDto() { Email = ChangeForm.Email, Password = ChangeForm.NewPassword });
                    await loginService.Login(LoginResponse.Token);
                    navManager.NavigateTo("");
                }
                else
                {
                    throw new Exception("ErroreConfermaPassword");
                }
            }
            catch (Exception e)
            {
                IsSubmitting = false;

                if (e.Message == "ErroreConfermaPassword")
                    Error = e.Message;
                else
                    Error = "ErroreCambioPw";
            }
        }
    }
}