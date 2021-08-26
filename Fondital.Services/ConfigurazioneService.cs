using System.Collections.Generic;
using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Services;

namespace Fondital.Services
{
    public class ConfigurazioneService : IConfigurazioneService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConfigurazioneService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Configurazione>> GetAllConfigurazioni()
        {
            return await _unitOfWork.Configurazioni.GetAllAsync();
        }

        public async Task<string> GetValoreByChiave(string chiave)
        {
            var config = await _unitOfWork.Configurazioni.SingleOrDefaultAsync(c => c.Chiave == chiave);
            return config.Valore;            
        }

        public async Task UpdateValore(Configurazione config)
        {
            _unitOfWork.Configurazioni.SingleOrDefaultAsync(c => c.Chiave == config.Chiave).Result.Valore = config.Valore;

            await _unitOfWork.CommitAsync();
        }
    }
}