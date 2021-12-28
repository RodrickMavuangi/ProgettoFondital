using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class ServicePartnerRepository : Repository<ServicePartner>, IServicePartnerRepository
    {
        public ServicePartnerRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<ServicePartner> GetWithUtenteAsync(int id)
        {
            return await Db.ServicePartners.Include(m => m.Utenti).SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<ServicePartner>> GetAllServicePartner()
        {
            return await Db.ServicePartners.Include(m => m.Utenti).ToListAsync();
        }
    }
}