using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models.Auth;

namespace Fondital.Shared.Services
{
    public interface IUtenteService
    {
        Task<IEnumerable<Utente>> GetAllUtenti();
        Task<Utente> GetUtenteById(int id);
        Task<Utente> CreateUtente(Utente utente);
        Task UpdateUtente(Utente utenteToBeUpdated, Utente utente);
        Task DeleteUtente(Utente utente);
    }
}