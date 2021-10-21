using Fondital.Shared;
using Fondital.Shared.Enums;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Services
{
    public class RapportoService : IRapportoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RapportoService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rapporto>> GetAllRapporti()
        {
            return await _unitOfWork.Rapporti.GetAllRapporti();
        }

        public async Task<Rapporto> GetRapportoById(int id)
        {
            return await _unitOfWork.Rapporti.GetByIdAsync(id);
        }

        public async Task UpdateRapporto(int rapportoId, Rapporto rapporto, Utente updatingUser)
        {
            var RapportoToUpdate = await _unitOfWork.Rapporti.GetByIdAsync(rapportoId);
            StatoRapporto? NuovoStato = rapporto.Stato == RapportoToUpdate.Stato ? null : rapporto.Stato;

            await _unitOfWork.Rapporti.AddAudit(RapportoToUpdate, updatingUser, NuovoStato);
            _unitOfWork.Update(RapportoToUpdate, rapporto);
            _unitOfWork.Update(RapportoToUpdate.Cliente, rapporto.Cliente);
            _unitOfWork.Update(RapportoToUpdate.Caldaia, rapporto.Caldaia);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> AddRapporto(Rapporto rapporto)
        {
            await _unitOfWork.Rapporti.AddRapporto(rapporto);
            await _unitOfWork.Rapporti.AddAudit(rapporto, rapporto.Utente, StatoRapporto.Aperto, "Creazione rapporto");
            await _unitOfWork.CommitAsync();

            return rapporto.Id;
        }
    }
}