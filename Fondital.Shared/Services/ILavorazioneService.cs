using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Services
{
    public interface ILavorazioneService
    {
        Task<IEnumerable<Lavorazione>> GetAllLavorazioni(bool? isAbilitato = null);
        Task<Lavorazione> GetLavorazioneById(int id);
        Task UpdateLavorazione(int lavorazioneId, Lavorazione lavorazione);
        Task AddLavorazione(Lavorazione lavorazione);
    }
}
