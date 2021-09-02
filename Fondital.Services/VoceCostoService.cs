using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;

namespace Fondital.Services
{
    public class VoceCostoService : IVoceCostoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VoceCostoService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VoceCosto>> GetAllVociCosto(bool? isAbilitato = null)
        {
            return await _unitOfWork.VociCosto.GetAllAsync(isAbilitato);
        }

        public async Task<VoceCosto> GetVoceCostoById(int id)
        {
            return await _unitOfWork.VociCosto.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateVoceCosto(int voceCostoId, VoceCosto voceCosto)
        {
            var voceCostoToUpdate = await _unitOfWork.VociCosto.SingleOrDefaultAsync(c => c.Id == voceCostoId);
            _unitOfWork.Update(voceCostoToUpdate, voceCosto);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task AddVoceCosto(VoceCosto voceCosto)
        {
            await _unitOfWork.VociCosto.AddAsync(voceCosto);
            await _unitOfWork.CommitAsync();
        }
    }
}