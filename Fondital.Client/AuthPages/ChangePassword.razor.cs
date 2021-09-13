using Fondital.Shared.Resources;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.AuthPages
{
    public partial class ChangePassword
    {
        [Parameter]
        public string Email { get; set; }
        private LoginResponse loginResponse { get; set; }
        public ChangePwRequest ChangeForm { get; set; } = new ChangePwRequest();
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
                    loginResponse = await authClient.Login(new LoginRequest() { Email = ChangeForm.Email, Password = ChangeForm.NewPassword });
                    await loginService.Login(loginResponse.Token);
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
