using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Fondital.Data;
using System.Linq;
using Fondital.Shared.Models.Auth;

namespace Fondital.Data.Repositories
{
    public class UtenteRepository : Repository<Utente>, IUtenteRepository
    {
        public UtenteRepository(FonditalDbContext context)
            : base(context)
        { }
        

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<Utente> GetByUsernameAsync(string username)
        {
            return await _db.Utenti.SingleOrDefaultAsync(u => u.UserName == username);
        }

    }
}