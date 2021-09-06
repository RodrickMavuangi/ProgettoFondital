using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;

namespace Fondital.Data.Repositories
{
    public class VoceCostoRepository : Repository<VoceCosto>, IVoceCostoRepository
    {
        public VoceCostoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<IEnumerable<VoceCosto>> GetAllAsync(bool? isAbilitato)
        {
            if (isAbilitato != null)
                return await _db.VociCosto.Where(x => x.IsAbilitato == isAbilitato).ToListAsync();
            else
                return await _db.VociCosto.ToListAsync();
        }
    }
}