using Fondital.Data;
using Fondital.Shared;
using Fondital.Shared.Repositories;
using System;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FonditalDbContext _context;
        private UtenteRepository _utenteRepository;
        private ServicePartnerRepository _servicePartnerRepository;
        private ConfigurazioneRepository _configurazioneRepository;
        private DifettoRepository _difettoRepository;
        private VoceCostoRepository _voceCostoRepository;
        private ListinoRepository _listinoRepository;
        private LavorazioneRepository _lavorazioneRepository;

        public UnitOfWork(FonditalDbContext context)
        {
            this._context = context;
        }

        public IUtenteRepository Utenti => _utenteRepository ??= new UtenteRepository(_context);
        public IServicePartnerRepository ServicePartners => _servicePartnerRepository ??= new ServicePartnerRepository(_context);
        public IConfigurazioneRepository Configurazioni => _configurazioneRepository ??= new ConfigurazioneRepository(_context);
        public IDifettoRepository Difetti => _difettoRepository ??= new DifettoRepository(_context);
        public IVoceCostoRepository VociCosto => _voceCostoRepository ??= new VoceCostoRepository(_context);
        public IListinoRepository Listini => _listinoRepository ??= new ListinoRepository(_context);
        public ILavorazioneRepository Lavorazioni => _lavorazioneRepository ??= new LavorazioneRepository(_context);

        public void Update<T>(T oldItem, T newItem) where T : class
        {
            this._context.Entry(oldItem).CurrentValues.SetValues(newItem);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}