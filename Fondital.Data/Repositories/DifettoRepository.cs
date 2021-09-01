using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;

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

        //public async Task<IEnumerable<Difetto>> GetByPage(int page, int pageSize, bool? isAbilitato)
        //{
        //    if (isAbilitato != null)
        //        return await _db.Difetti.Where(x => x.IsAbilitato == isAbilitato).Skip(page * pageSize).Take(pageSize).ToListAsync();
        //    else
        //        return await _db.Difetti.Skip(page * pageSize).Take(pageSize).ToListAsync();
        //}
    }
}