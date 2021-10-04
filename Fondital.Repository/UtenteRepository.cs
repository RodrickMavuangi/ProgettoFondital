using Fondital.Data;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class UtenteRepository : Repository<Utente>, IUtenteRepository
    {
        public UtenteRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<Utente> GetByUsernameAsync(string username)
        {
            return await Db.Utenti.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<Utente>> GetAllUtentiWithRoles()
        {
            List<Utente> Utenti = new();
            try
            {
                return await Db.Utenti.Include(m => m.Ruoli).ToListAsync();
            }
            catch (Exception e) { }

            return Utenti;
        }
    }
}