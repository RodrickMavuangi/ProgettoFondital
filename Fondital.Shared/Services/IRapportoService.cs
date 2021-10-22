using Fondital.Shared.Enums;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface IRapportoService
    {
        Task<IEnumerable<Rapporto>> GetAllRapporti();
        Task<Rapporto> GetRapportoById(int id);
        Task UpdateRapporto(int rapportoId, Rapporto rapporto, Utente updatingUser);
        Task<int> AddRapporto(Rapporto rapporto);
    }
}