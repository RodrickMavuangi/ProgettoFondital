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
        public ChangePwRequestDto ChangeForm { get; set; } = new ChangePwRequestDto();
        public string Error { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ChangeForm.Email = Email;
            Error = localizer["PasswordMustChange"];
        }

        protected async Task ChangePw()
        {

            try
            {
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
                if (e.Message == "ErroreConfermaPassword")
                    Error = e.Message;
                else
                    Error = "ErroreCambioPw";
            }
        }
    }
}