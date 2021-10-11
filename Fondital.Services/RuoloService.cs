using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Services
{
    public class RuoloService : IRuoloService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RuoloService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Ruolo>> GetAll()
        {
            return await _unitOfWork.Ruoli.GetAllAsync();
        }
    }
}