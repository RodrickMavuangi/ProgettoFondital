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

        public async Task<int> CreateServicePartner(ServicePartner newSp)
        {
            await _unitOfWork.ServicePartners
                .AddAsync(newSp);
            await _unitOfWork.CommitAsync();

            return newSp.Id;
        }

        public async Task DeleteServicePartner(ServicePartner sp)
        {
            _unitOfWork.ServicePartners.Remove(sp);

            await _unitOfWork.CommitAsync();
        }

        public async Task<ServicePartner> GetServicePartnerWithUtentiAsync(int ServicePartnerID)
		{
            return await _unitOfWork.ServicePartners.GetWithUtenteAsync(ServicePartnerID);
		}

        public async Task<IEnumerable<ServicePartner>> GetAllServicePartners()
        {
           // return await _unitOfWork.ServicePartners.GetAllAsync();
            return await _unitOfWork.ServicePartners.GetAllServicePartner();
        }

        
        public async Task<ServicePartner> GetServicePartnerById(int id)
        {
            return await _unitOfWork.ServicePartners.GetByIdAsync(id);
        }

        public async Task UpdateServicePartner(int spId, ServicePartner sp)
        {
            var spToUpdate = await _unitOfWork.ServicePartners.SingleOrDefaultAsync(c => c.Id == spId);
            _unitOfWork.Update(spToUpdate, sp);

            if(spToUpdate.Utenti != null && sp.Utenti != null)
			{
                foreach (var item in sp.Utenti)
				{
                    spToUpdate.Utenti.Add(item);
				}
			}
            if(spToUpdate.Utenti == null && sp.Utenti != null)
			{
                spToUpdate.Utenti = new List<Utente>();
                foreach (var item in sp.Utenti)
                {
                    spToUpdate.Utenti.Add(item);
                }
            }
            await _unitOfWork.CommitAsync();
        }                                       
    }
}