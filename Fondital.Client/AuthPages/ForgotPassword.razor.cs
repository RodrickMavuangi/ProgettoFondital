using Fondital.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Telerik.Blazor;


namespace Fondital.Client.AuthPages
{
    public partial class ForgotPassword
    {
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        private LoginResponseDto loginResponse { get; set; }
        public ForgotPwRequestDto ForgotForm { get; set; } = new ForgotPwRequestDto();
        public string Error { get; set; }
        protected override async Task OnInitializedAsync()
        {
           Error = localizer["RitrovaAccount"];
        }

        protected async Task ForgotPw()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ForgotForm.Email))
                {
                    await authClient.ForgotPassword(ForgotForm);
                    await Dialogs.AlertAsync($"{@localizer["MailInviata"]} {ForgotForm.Email} {@localizer["ResetPassword"]}");
                }
                else
                {
                    throw new Exception("CampoObbligatorio");
                }
            }
            catch (Exception e)
            {
                if (e.Message == "CampoObbligatorio")
                    Error = e.Message;
                else
                    Error = "ErroreEmail";
            }
        }
    }
}