using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface IDifettoService
    {
        Task<IEnumerable<Difetto>> GetAllDifetti(bool? isAbilitato = null);
        //Task<IEnumerable<Difetto>> GetDifettiByPage(int page, int pageSize, bool? isAbilitato = null);
        Task<Difetto> GetDifettoById(int id);
        Task UpdateDifetto(int difettoId, Difetto difetto);
        Task AddDifetto(Difetto difetto);
    }
}