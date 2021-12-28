using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class VoceCostoRepository : Repository<VoceCosto>, IVoceCostoRepository
    {
        public VoceCostoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<IEnumerable<VoceCosto>> GetAllAsync(bool? isAbilitato)
        {
            if (isAbilitato != null)
                return await Db.VociCosto.Where(x => x.IsAbilitato == isAbilitato).ToListAsync();
            else
                return await Db.VociCosto.ToListAsync();
        }
    }
}