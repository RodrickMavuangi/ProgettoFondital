using Fondital.Shared.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IUtenteRepository : IRepository<Utente>
    {
        Task<Utente> GetByUsernameAsync(string username);
        Task<IEnumerable<Utente>> GetAllUtenti(bool? isDirezione);
        Task CreateUtente(Utente utente);
    }
}