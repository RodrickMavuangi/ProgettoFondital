using Fondital.Data;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class RuoloRepository : Repository<Ruolo>, IRuoloRepository
    {
        public RuoloRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<IEnumerable<Ruolo>> GetAll()
        {
            return await Db.Roles.ToListAsync();
        }
    }
}