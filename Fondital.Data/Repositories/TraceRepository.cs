using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;

namespace Fondital.Data.Repositories
{
    public class TraceRepository : Repository<Trace>, ITraceRepository
    {
        public TraceRepository(FonditalDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Trace>> GetAllWithUtenteAsync()
        {
            return await _db.Traces
                .Include(m => m.Utente)
                .ToListAsync();
        }

        public async Task<Trace> GetWithUtenteByIdAsync(int id)
        {
            return await _db.Traces
                .Include(m => m.Utente)
                .SingleOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task<IEnumerable<Trace>> GetAllWithUtenteByUtenteIdAsync(int utenteId)
        {
            return await _db.Traces
                .Include(m => m.Utente)
                //.Where(m => m.Utente.Id == utenteId)
                .ToListAsync();
        }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }
    }
}