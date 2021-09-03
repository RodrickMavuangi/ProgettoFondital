using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;

namespace Fondital.Data.Repositories
{
    public class ListinoRepository : Repository<Listino>, IListinoRepository
    {
        public ListinoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<Listino> GetListinoByIdAsync(int Id)
        {
            return await _db.Listini.Include( x => x.ServicePartner).Include( x => x.VoceCosto).SingleOrDefaultAsync( x => x.Id == Id);
        }
    }
}