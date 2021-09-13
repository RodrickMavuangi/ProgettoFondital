using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Resources;
using System.Net.Http.Json;

namespace Fondital.Client.Pages
{
    [AllowAnonymous]
    public partial class Login
    {
        LoginRequest model = new LoginRequest();
        LoginResponse loginResponse = null;

        public async Task UserLogin()
        {
            loginResponse = await authClient.Login(model);
            if (loginResponse.IsSuccess)
            {
                await loginService.Login(loginResponse.Token);
                navManager.NavigateTo("");
            }
        }
    }
}