using Fondital.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Shared.Repositories
{
    public interface IServicePartnerRepository : IRepository<ServicePartner>
    {
        Task<ServicePartner> GetWithUtenteAsync(int ServicePartnerId);
        Task<IEnumerable<ServicePartner>> GetAllServicePartner();
    }
}