using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;

namespace Fondital.Services
{
    public class DifettoService : IDifettoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DifettoService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Difetto>> GetAllDifetti(bool? isAbilitato = null)
        {
            return await _unitOfWork.Difetti.GetAllAsync(isAbilitato);
        }

        public async Task<Difetto> GetDifettoById(int id)
        {
            return await _unitOfWork.Difetti.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateDifetto(int difettoId, Difetto difetto)
        {
            var difettoToUpdate = await _unitOfWork.Difetti.SingleOrDefaultAsync(c => c.Id == difettoId);
            _unitOfWork.Update(difettoToUpdate, difetto);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task AddDifetto(Difetto difetto)
        {
            await _unitOfWork.Difetti.AddAsync(difetto);
            await _unitOfWork.CommitAsync();
        }
    }
}