using AutoMapper;
using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using Fondital.Shared.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
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
        private readonly IRuoloService _ruoloService;
        private readonly IMapper _mapper;
        public AuthController(Serilog.ILogger logger, UserManager<Utente> userManager, RoleManager<Ruolo> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings, IAuthService authService, IConfigurazioneService confService, IUtenteService utenteService, SignInManager<Utente> signInManager, IRuoloService ruoloService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _authService = authService;
            _confService = confService;
            _utenteService = utenteService;
            _signinManager = signInManager;
            _logger = logger;
            _ruoloService = ruoloService;
            _mapper = mapper;

        }

        [HttpPost("CreateWithPassword")]
        //[Authorize("Direzione")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateWithPassword(Utente user, string password)
        {
            var x = await _userManager.CreateAsync(user, password);
            if (x.Succeeded)
                return Ok();
            else
                return BadRequest(x.Errors);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LoginRequestDto loginRequest)
        {
            _logger.Information("Log di prova per l'utente con email {LoginRequestEmail}", loginRequest.Email);
            LoginResponseDto response = new();
            try
            {
                var result = await _signinManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);

                if (!result.Succeeded)
                {
                    return BadRequest("ErroreUserPassword");
                }

                var user = await _signinManager.UserManager.FindByEmailAsync(loginRequest.Email);
                var roles = await _signinManager.UserManager.GetRolesAsync(user);
                int durataPasswordInGiorni = 30 * (int)Enum.Parse<DurataValiditaConfigurazione>(_confService.GetValoreByChiave("DurataPassword").Result);

                if (user.Pw_MustChange || (DateTime.Now - user.Pw_LastChanged).TotalDays > durataPasswordInGiorni)
                {
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
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error("Login error: {Message} - Email: {LoginRequestEmail}", ex.Message, loginRequest.Email);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("changepw")]
        [AllowAnonymous]
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
                    return Ok();
                else
                    return BadRequest(task.Errors);
            }
            catch (Exception ex)
            {
                _logger.Error($"Change password error: {ex.Message} - Email: {ChangePwRequest.Email}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("resetpw")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPwRequestDto resetPasswordRequest)
        {
            try
            {
                var user = await _signinManager.UserManager.FindByEmailAsync(resetPasswordRequest.Email);
                user.Pw_LastChanged = DateTime.Now;
                user.Pw_MustChange = false;
                //await _utenteService.UpdateUtente(user.UserName, user);

                string Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordRequest.Token));
                var task = await _userManager.ResetPasswordAsync(user, Token, resetPasswordRequest.ConfirmPassword);
                if (task.Succeeded)
                    return Ok();
                else
                    return BadRequest(task.Errors);
            }
            catch (Exception e) { throw; }
        }

        [HttpPost("ruolo/{UtenteEmail}")]
        [AllowAnonymous]
        public async Task<IActionResult> AssegnaRuolo(string UtenteEmail, [FromBody]RuoloDto ruolo)
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

            catch(Exception e)
			{
                throw;
			}
		}

        //[HttpPost("Roles")]
        //public async Task<IActionResult> CreateRole(string roleName)
        //{
        //    if (string.IsNullOrWhiteSpace(roleName))
        //    {
        //        return BadRequest("Role name should be provided.");
        //    }
        //
        //    var newRole = new Ruolo
        //    {
        //        Name = roleName
        //    };
        //
        //    var roleResult = await _roleManager.CreateAsync(newRole);
        //
        //    if (roleResult.Succeeded)
        //    {
        //        return Ok();
        //    }
        //
        //    return Problem(roleResult.Errors.First().Description, null, 500);
        //}

        //[HttpPost("User/{userEmail}/Role")]
        //public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        //{
        //    var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);
        //
        //    var result = await _userManager.AddToRoleAsync(user, roleName);
        //
        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //
        //    return Problem(result.Errors.First().Description, null, 500);
        //}

        private string GenerateJwt(Utente user, IList<string> roles, JwtSettings jwtSettings)
        {
            return _authService.GeneraJwt(user, roles, jwtSettings);
        }
    }
}