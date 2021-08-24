using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Models.Auth;
using Fondital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Fondital.Services;

namespace Fondital.Server.Areas.Identity.Pages.Account.Manage
{
    [AllowAnonymous]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<Utente> _userManager;
        private readonly SignInManager<Utente> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly UtenteService _utService;

        public ChangePasswordModel(
            UserManager<Utente> userManager,
            SignInManager<Utente> signInManager,
            ILogger<ChangePasswordModel> logger,
            UtenteService utService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _utService = utService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password attuale")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(40, ErrorMessage = "La {0} deve essere lunga almeno {2} e al più {1} caratteri. Deve contenere maiuscole, minuscole, numeri e simboli.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Nuova password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Conferma nuova password")]
            [Compare("NewPassword", ErrorMessage = "La nuova password e la conferma nuova password devono coincidere.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl, string username)
        {
            ReturnUrl = returnUrl;
            Username = username;
            var user = await _userManager.FindByNameAsync(Username);
            
            if (user == null)
            {
                return NotFound($"Impossibile caricare un utente con username '{Username}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
            {
                return NotFound($"Impossibile caricare un utente con username '{Username}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            else
            {
                user.Pw_LastChanged = DateTime.Now;
                user.Pw_MustChange = false;
                await _utService.UpdateUtente(Username, user);
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "La tua password è stata cambiata con successo.";

            return LocalRedirect(ReturnUrl);
        }
    }
}
