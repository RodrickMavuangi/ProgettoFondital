using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models.Auth;

namespace Fondital.Shared.Services
{
    public interface IUtenteService
    {
        Task<IEnumerable<Utente>> GetAllUtenti();
        Task<Utente> GetUtenteById(int id);
        Task<Utente> GetUtenteByUsername(string username);
        Task<Utente> CreateUtente(Utente utente);
        Task UpdateUtente(string username, Utente utFromDB);
        Task DeleteUtente(Utente utente);
    }
}