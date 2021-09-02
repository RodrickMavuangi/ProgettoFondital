using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface IServicePartnerService
    {
        Task<IEnumerable<ServicePartner>> GetAllServicePartners();
        Task<ServicePartner> GetServicePartnerById(int id);
        Task<ServicePartner> GetServicePartnerByIdWithUtenti(int id);
        Task<ServicePartner> CreateServicePartner(ServicePartner sp);
        Task UpdateServicePartner(ServicePartner spToBeUpdated, ServicePartner sp);
        Task DeleteServicePartner(ServicePartner sp);
    }
}