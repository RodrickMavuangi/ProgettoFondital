using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;

namespace Fondital.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UtenteService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Utente> CreateUtente(Utente newUser)
        {
            await _unitOfWork.Utenti
                .AddAsync(newUser);
            await _unitOfWork.CommitAsync();

            return newUser;
        }

        public async Task DeleteUtente(Utente utente)
        {
            _unitOfWork.Utenti.Remove(utente);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Utente>> GetAllUtenti()
        {
            return await _unitOfWork.Utenti.GetAllAsync();
        }

        public async Task<Utente> GetUtenteById(int id)
        {
            return await _unitOfWork.Utenti.GetByIdAsync(id);
        }

        public async Task UpdateUtente(Utente utenteToUpdate, Utente utente)
        {
            utenteToUpdate.Nome = utente.Nome;

            await _unitOfWork.CommitAsync();
        }
    }
}