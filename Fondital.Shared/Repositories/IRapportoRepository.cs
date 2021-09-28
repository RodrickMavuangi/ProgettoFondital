using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IRapportoRepository : IRepository<Rapporto>
    {
        Task<List<Rapporto>> GetAllRapporti();
        Task<Rapporto> GetRapportoByIdAsync(int Id);
        Task AddRapporto(Rapporto rapporto);
    }
}
