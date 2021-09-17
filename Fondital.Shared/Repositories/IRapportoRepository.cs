using Fondital.Shared.Models;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IRapportoRepository : IRepository<Rapporto>
    {
        Task<Rapporto> GetRapportoByIdAsync(int Id);
    }
}
