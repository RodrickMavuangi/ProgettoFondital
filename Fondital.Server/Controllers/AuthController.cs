using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Shared.Models.Auth;
using Fondital.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fondital.Shared.Services;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;
using Fondital.Shared.Resources;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        private readonly SignInManager<Utente> _signinManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, UserManager<Utente> userManager, RoleManager<Ruolo> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings, IAuthService authService, SignInManager<Utente> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _authService = authService;
            _signinManager = signInManager;
            _configuration = configuration;
            _logger = logger;
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
        public async Task<IActionResult> LogIn([FromBody] LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                var result = await _signinManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);

                if (!result.Succeeded)
                {
                    response.Errors = new List<string> { "ErroreUserPassword." };
                    return Ok(response);
                }

                var user = await _signinManager.UserManager.FindByEmailAsync(loginRequest.Email);
                var roles = await _signinManager.UserManager.GetRolesAsync(user);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, loginRequest.Email));

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                response.Token = GenerateJwt(user, roles, _jwtSettings);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Login error: {ex.Message} - Email: {loginRequest.Email}");
                response.Errors = new List<string> { ex.Message };
                return Ok(response);
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

        [AllowAnonymous]
        private string GenerateJwt(Utente user, IList<string> roles, JwtSettings jwtSettings)
        {
            return _authService.GeneraJwt(user, roles, jwtSettings);
        }
    }
}
