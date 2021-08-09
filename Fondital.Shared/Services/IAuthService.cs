using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface IAuthService
    {
        string GeneraJwt(Utente user, IList<string> roles, JwtSettings jwtSettings);
    }
}
