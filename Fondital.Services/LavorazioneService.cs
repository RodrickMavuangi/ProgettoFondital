using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Services
{
    public class LavorazioneService : ILavorazioneService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LavorazioneService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Lavorazione>> GetAllLavorazioni(bool? isAbilitato = null)
        {
            return await _unitOfWork.Lavorazioni.GetAllAsync(isAbilitato);
        }

        public async Task<Lavorazione> GetLavorazioneById(int id)
        {
            return await _unitOfWork.Lavorazioni.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateLavorazione(int lavorazioneId, Lavorazione lavorazione)
        {
            var lavorazioneToUpdate = await _unitOfWork.Lavorazioni.SingleOrDefaultAsync(c => c.Id == lavorazioneId);
            _unitOfWork.Update(lavorazioneToUpdate, lavorazione);

            await _unitOfWork.CommitAsync();
        }

        public async Task<int> AddLavorazione(Lavorazione lavorazione)
        {
            await _unitOfWork.Lavorazioni.AddAsync(lavorazione);
            await _unitOfWork.CommitAsync();

            return lavorazione.Id;
        }
    }
}