using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface IConfigurazioneService
    {
        Task<IEnumerable<Configurazione>> GetAllConfigurazioni();
        Task<string> GetValoreByChiave(string chiave);
        Task UpdateValore(Configurazione configurazione);
    }
}