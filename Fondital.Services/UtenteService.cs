using Fondital.Shared;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            await _unitOfWork.Utenti.CreateUtente(newUser);
            await _unitOfWork.CommitAsync();

            return newUser;
        }

        public async Task DeleteUtente(Utente utente)
        {
            _unitOfWork.Utenti.Remove(utente);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Utente>> GetAllUtenti(bool? isDirezione)
        {
            return await _unitOfWork.Utenti.GetAllUtenti(isDirezione);
        }

        public async Task<Utente> GetUtenteById(int id)
        {
            return await _unitOfWork.Utenti.GetByIdAsync(id);
        }

        public async Task<Utente> GetUtenteByUsername(string username)
        {
            return await _unitOfWork.Utenti.GetByUsernameAsync(username);
        }

        public async Task UpdateUtente(string username, Utente utToUpdate)
		{
            //EF Core da problemi con i campi di Identity, per questo non viene chiamato l'Update di UnitOfWork
            var utFromDB = await _unitOfWork.Utenti.SingleOrDefaultAsync(u => u.UserName == username);
            utFromDB.Cognome = utToUpdate.Cognome;
            utFromDB.Nome = utToUpdate.Nome;
            utFromDB.IsAbilitato = utToUpdate.IsAbilitato;
            await _unitOfWork.CommitAsync();
		}
    }
}