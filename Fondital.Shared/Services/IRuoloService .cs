using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Dto;
using Fondital.Shared.Models.Auth;

namespace Fondital.Shared.Services
{
    public interface IRuoloService
    {
        Task<IEnumerable<Ruolo>> GetAll();
    }
}