using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface IRapportoService
    {
        Task<IEnumerable<Rapporto>> GetAllRapporti();
        Task<Rapporto> GetRapportoById(int id);
        Task UpdateRapporto(int rapportoId, Rapporto rapporto);
        Task AddRapporto(Rapporto rapporto);
    }
}
