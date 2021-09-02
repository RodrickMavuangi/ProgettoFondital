using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;

namespace Fondital.Services
{
    public class ServicePartnerService : IServicePartnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServicePartnerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ServicePartner> CreateServicePartner(ServicePartner newSp)
        {
            await _unitOfWork.ServicePartners
                .AddAsync(newSp);
            await _unitOfWork.CommitAsync();

            return newSp;
        }

        public async Task DeleteServicePartner(ServicePartner sp)
        {
            _unitOfWork.ServicePartners.Remove(sp);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ServicePartner>> GetAllServicePartners()
        {
            return await _unitOfWork.ServicePartners.GetAllAsync();
        }

        public async Task<ServicePartner> GetServicePartnerById(int id)
        {
            return await _unitOfWork.ServicePartners.GetByIdAsync(id);
        }

        public async Task UpdateServicePartner(ServicePartner spToUpdate, ServicePartner sp)
        {
            _unitOfWork.Update(spToUpdate, sp);

            await _unitOfWork.CommitAsync();
        }

        public async Task<ServicePartner> GetServicePartnerByIdWithUtenti(int id)
        {
            return await _unitOfWork.ServicePartners.GetByIdWithUtenti(id);
        }
    }
}