using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Data.Repositories
{
    public class LavorazioneRepository : Repository<Lavorazione>, ILavorazioneRepository
    {
        public LavorazioneRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<IEnumerable<Lavorazione>> GetAllAsync(bool? isAbilitato)
        {
            if (isAbilitato != null)
                return await _db.Lavorazioni.Where(x => x.IsAbilitato == isAbilitato).ToListAsync();
            else
                return await _db.Lavorazioni.ToListAsync();
        }
    }
}
