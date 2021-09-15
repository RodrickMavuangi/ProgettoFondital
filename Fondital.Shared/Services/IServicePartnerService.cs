using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface IServicePartnerService
    {
        Task<IEnumerable<ServicePartner>> GetAllServicePartners();
        Task<ServicePartner> GetServicePartnerWithUtentiAsync(int ServicePartnerID);
        Task<ServicePartner> GetServicePartnerById(int id);
        Task<ServicePartner> CreateServicePartner(ServicePartner sp);
        Task UpdateServicePartner(int spId, ServicePartner sp);
        Task DeleteServicePartner(ServicePartner sp);
    }
}