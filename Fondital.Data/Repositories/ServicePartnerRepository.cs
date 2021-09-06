using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;
using Fondital.Shared.Models.Auth;
using System;

namespace Fondital.Data.Repositories
{
    public class ServicePartnerRepository : Repository<ServicePartner>, IServicePartnerRepository
    {
        public ServicePartnerRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<ServicePartner> GetWithUtenteAsync(int id)
		{
            return await _db.ServicePartners.Include(m => m.Utenti).SingleOrDefaultAsync(m => m.Id == id);
		}

        public async Task<IEnumerable<ServicePartner>> GetAllServicePartner()
		{
            List<ServicePartner> servicePartnersRet = new List<ServicePartner>();
			try
			{
               return await _db.ServicePartners.Include(m => m.Utenti).ToListAsync();
    		}
            catch(Exception e) { }

            return servicePartnersRet;
        }
    }
}