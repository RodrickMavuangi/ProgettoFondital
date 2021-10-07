using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Fondital.Shared.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Server.Controllers
{
    [Route("authControl")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Utente> _userManager;
        private readonly RoleManager<Ruolo> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IAuthService _authService;
        private readonly IConfigurazioneService _confService;
        private readonly IUtenteService _utenteService;
        private readonly SignInManager<Utente> _signinManager;
        private readonly Serilog.ILogger _logger;

        public AuthController(Serilog.ILogger logger, UserManager<Utente> userManager, RoleManager<Ruolo> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings, IAuthService authService, IConfigurazioneService confService, IUtenteService utenteService, SignInManager<Utente> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _authService = authService;
            _confService = confService;
            _utenteService = utenteService;
            _signinManager = signInManager;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginRequestDto loginRequest)
        {
            LoginResponseDto response = new();
            try
            {
                var result = await _signinManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);

                if (!result.Succeeded)
                {
                    _logger.Information("Login failure: {Action} {Object} {ObjectId} fallito per {LoginFailureReason}", "LOGIN", "Utente", loginRequest.Email, "credenziali errate");
                    return BadRequest("ErroreUserPassword");
                }

                var user = await _signinManager.UserManager.FindByEmailAsync(loginRequest.Email);
                var roles = await _signinManager.UserManager.GetRolesAsync(user);
                int durataPasswordInGiorni = 30 * (int)Enum.Parse<DurataValiditaConfigurazione>(_confService.GetValoreByChiave("DurataPassword").Result);

                if (!user.IsAbilitato)
                {
                    _logger.Information("Login failure: {Action} {Object} {ObjectId} fallito per {LoginFailureReason}", "LOGIN", "Utente", loginRequest.Email, "utente disabilitato");
                    return BadRequest("UtenteDisabilitato");
                }
                else if (user.Pw_MustChange || (DateTime.Now - user.Pw_LastChanged).TotalDays > durataPasswordInGiorni)
                {
                    _logger.Information("Login failure: {Action} {Object} {ObjectId} fallito per {LoginFailureReason}", "LOGIN", "Utente", loginRequest.Email, "password scaduta");
                    return BadRequest("PasswordMustChange");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginRequest.Email)
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                response.Token = GenerateJwt(user, roles, _jwtSettings);

                _logger.Information("Login success: {Action} {Object} {ObjectId} effettuato con successo", "LOGIN", "Utente", loginRequest.Email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjecId}: {ExceptionMessage}", "LOGIN", "Utente", loginRequest.Email, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("changepw")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePwRequestDto ChangePwRequest)
        {
            try
            {
                var user = await _signinManager.UserManager.FindByEmailAsync(ChangePwRequest.Email);
                user.Pw_LastChanged = DateTime.Now;
                user.Pw_MustChange = false;
                await _utenteService.UpdateUtente(user.UserName, user);

                var task = await _userManager.ChangePasswordAsync(user, ChangePwRequest.OldPassword, ChangePwRequest.NewPassword);
                if (task.Succeeded)
                {
                    _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "CHANGEPWD", "Utente", ChangePwRequest.Email);
                    return Ok();
                }
                else
                    return BadRequest(task.Errors);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "CHANGEPWD", "Utente", ChangePwRequest.Email, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("resetpw")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPwRequestDto resetPasswordRequest)
        {
            try
            {
                var user = await _signinManager.UserManager.FindByEmailAsync(resetPasswordRequest.Email);
                user.Pw_LastChanged = DateTime.Now;
                user.Pw_MustChange = false;

                string Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordRequest.Token));
                var task = await _userManager.ResetPasswordAsync(user, Token, resetPasswordRequest.ConfirmPassword);
                if (task.Succeeded)
                {
                    _logger.Information("Info: {Action} {Object} {ObjectId} effettuato con successo", "RESETPWD", "Utente", resetPasswordRequest.Email);
                    return Ok();
                }
                else
                    return BadRequest(task.Errors);
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "RESETPWD", "Utente", resetPasswordRequest.Email, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ruolo/{UtenteEmail}")]
        [Authorize(Roles = "Direzione")]
        public async Task<IActionResult> AssegnaRuolo(string UtenteEmail, [FromBody] RuoloDto ruolo)
        {
            try
            {
                IdentityResult res = new IdentityResult();

                var user = await _userManager.FindByEmailAsync(UtenteEmail);

                //Si aggiunge un ruolo nel db solo se non esiste ancora
                if (!await _roleManager.RoleExistsAsync(ruolo.Name))
                {
                    Ruolo r = new Ruolo() { Name = ruolo.Name };
                    r.Utenti.Add(user);
                    res = await _roleManager.CreateAsync(r);
                    if (!res.Succeeded) BadRequest(res.Errors);
                }
                else
                {
                    // Aggiorna la lista di utente del ruolo
                    Ruolo r = await _roleManager.FindByNameAsync(ruolo.Name);
                    r.Utenti.Add(user);
                    res = await _roleManager.UpdateAsync(r);
                    if (!res.Succeeded) BadRequest(res.Errors);
                }

                await _userManager.AddToRoleAsync(user, ruolo.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("Eccezione {Action} {Object} {ObjectId}: {ExceptionMessage}", "Assegna", "Ruolo", ruolo.Name, ex.Message);
                throw;
            }
        }

        private string GenerateJwt(Utente user, IList<string> roles, JwtSettings jwtSettings)
        {
            return _authService.GeneraJwt(user, roles, jwtSettings);
        }
    }
}