using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class ListinoRepository : Repository<Listino>, IListinoRepository
    {
        public ListinoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<Listino> GetListinoByIdAsync(int Id)
        {
            return await Db.Listini.Include(x => x.ServicePartner).Include(x => x.VoceCosto).SingleOrDefaultAsync(x => x.Id == Id);
        }
    }
}