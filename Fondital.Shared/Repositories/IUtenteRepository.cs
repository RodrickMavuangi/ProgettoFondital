using Fondital.Shared.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IUtenteRepository : IRepository<Utente>
    {
        Task<Utente> GetByUsernameAsync(string username);
        Task<IEnumerable<Utente>> GetAllUtentiWithRoles(Ruolo ruolo);
        Task CreateUtente(Utente utente);
    }
}