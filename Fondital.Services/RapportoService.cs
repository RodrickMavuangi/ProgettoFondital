using Fondital.Shared;
using Fondital.Shared.Models;
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
            return await _unitOfWork.Rapporti.GetAllAsync();
        }

        public async Task<Rapporto> GetRapportoById(int id)
        {
            return await _unitOfWork.Rapporti.GetByIdAsync(id);
        }

        public async Task UpdateRapporto(int rapportoId, Rapporto rapporto)
        {
            var rapportoToUpdate = await _unitOfWork.Rapporti.GetByIdAsync(rapportoId);
            _unitOfWork.Update(rapportoToUpdate, rapporto);

            await _unitOfWork.CommitAsync();
        }

        public async Task AddRapporto(Rapporto rapporto)
        {
            await _unitOfWork.Rapporti.AddAsync(rapporto);
            await _unitOfWork.CommitAsync();
        }
    }
}
