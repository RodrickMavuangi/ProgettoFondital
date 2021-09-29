using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Services
{
    public class ListinoService : IListinoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ListinoService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Listino>> GetAllListini()
        {
            return await _unitOfWork.Listini.GetAllListiniAsync();
        }

        public async Task<Listino> GetListinoById(int id)
        {
            return await _unitOfWork.Listini.GetListinoByIdAsync(id);
        }

        public async Task UpdateListino(int listinoId, Listino listino)
        {
            var listinoToUpdate = await _unitOfWork.Listini.GetByIdAsync(listinoId);
            _unitOfWork.Update(listinoToUpdate, listino);
            
            await _unitOfWork.CommitAsync();
        }

        public async Task AddListino(Listino listino)
        {
            await _unitOfWork.Listini.AddListino(listino);
            await _unitOfWork.CommitAsync();
        }
    }
}