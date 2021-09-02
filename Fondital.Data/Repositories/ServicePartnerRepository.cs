using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;

namespace Fondital.Data.Repositories
{
    public class ServicePartnerRepository : Repository<ServicePartner>, IServicePartnerRepository
    {
        public ServicePartnerRepository(FonditalDbContext context)
            : base(context)
        { }

        public async Task<ServicePartner> GetByIdWithUtenti(int id)
        {
            return await _db.ServicePartners.Include(sp => sp.Utenti).SingleOrDefaultAsync(sp => sp.Id == id);
        }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }
    }
}