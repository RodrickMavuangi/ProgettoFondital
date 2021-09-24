using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;


namespace Fondital.Client.AuthPages
{
    public partial class ResetPassword
    {
        [Parameter]
        public string Email { get; set; }
        [Parameter]
        public string Token { get; set; }
        private LoginResponseDto loginResponse { get; set; }
        public ResetPwRequestDto ResetForm { get; set; } = new ResetPwRequestDto();
        public string Error { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ResetForm.Email = Email;
            ResetForm.Token = Token;
            Error = localizer[""];
        }

        protected async Task ResetPw()
        {

            try
            {
                if (ResetForm.Password == ResetForm.ConfirmPassword)
                {
                    await authClient.ResetPassword(ResetForm);
                    loginResponse = await authClient.Login(new LoginRequestDto() { Email = ResetForm.Email, Password = ResetForm.Password });
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
                    Error = "Errore";
            }
        }
    }
}