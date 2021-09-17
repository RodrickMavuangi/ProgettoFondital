using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class RapportoRepository : Repository<Rapporto>, IRapportoRepository
    {
        public RapportoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<Rapporto> GetRapportoByIdAsync(int Id)
        {
            return await Db.Rapporti.Include(x => x.ServicePartner).Include(x => x.Utente).SingleOrDefaultAsync(x => x.Id == Id);
        }
    }
}
