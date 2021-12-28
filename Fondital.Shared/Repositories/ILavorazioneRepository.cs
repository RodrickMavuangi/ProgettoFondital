using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface ILavorazioneRepository : IRepository<Lavorazione>
    {
        Task<IEnumerable<Lavorazione>> GetAllAsync(bool? isAbilitato);
    }
}