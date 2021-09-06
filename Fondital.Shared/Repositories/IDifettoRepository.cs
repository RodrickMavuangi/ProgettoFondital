using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IDifettoRepository : IRepository<Difetto>
    {
        Task<IEnumerable<Difetto>> GetAllAsync(bool? isAbilitato);
        //Task<IEnumerable<Difetto>> GetByPage(int page, int pageSize, bool? isAbilitato);
    }
}