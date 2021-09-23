using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class ForgotPwRequestDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}