using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface IVoceCostoService
    {
        Task<IEnumerable<VoceCosto>> GetAllVociCosto(bool? isAbilitato = null);
        Task<VoceCosto> GetVoceCostoById(int id);
        Task UpdateVoceCosto(int difettoId, VoceCosto difetto);
        Task AddVoceCosto(VoceCosto difetto);
    }
}