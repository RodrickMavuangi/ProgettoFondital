using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fondital.Server.Resources;
using Fondital.Shared.Models.Auth;
using Fondital.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fondital.Shared.Services;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace Fondital.Server.Controllers
{
    [Route("authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IMapper _mapper; 
        private readonly UserManager<Utente> _userManager;
        private readonly RoleManager<Ruolo> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IAuthService _authService;

        public AuthController(UserManager<Utente> userManager, RoleManager<Ruolo> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings, IAuthService authService)
        {
            //_mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _authService = authService;
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(UserLoginResource userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);

            if (user is null)
            {
                return BadRequest("Email o password non corrette.");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(GenerateJwt(user, roles, _jwtSettings));
            }
            else
                return BadRequest("Email o password non corrette.");
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Ruolo
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        [HttpPost("User/{userEmail}/Role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }

        [AllowAnonymous]
        private string GenerateJwt(Utente user, IList<string> roles, JwtSettings jwtSettings)
        {
            return _authService.GeneraJwt(user, roles, jwtSettings);
        }
    }
}
