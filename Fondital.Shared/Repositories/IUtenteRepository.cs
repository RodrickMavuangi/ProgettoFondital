using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IUtenteRepository : IRepository<Utente>
    {
        Task<Utente> GetByUsernameAsync(string username);
        Task CreateUtente(Utente utente);
    }
}