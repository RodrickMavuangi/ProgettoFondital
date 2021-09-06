using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Data.Repositories
{
    public class DifettoRepository : Repository<Difetto>, IDifettoRepository
    {
        public DifettoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<IEnumerable<Difetto>> GetAllAsync(bool? isAbilitato)
        {
            if (isAbilitato != null)
                return await _db.Difetti.Where(x => x.IsAbilitato == isAbilitato).ToListAsync();
            else
                return await _db.Difetti.ToListAsync();
        }
    }
}