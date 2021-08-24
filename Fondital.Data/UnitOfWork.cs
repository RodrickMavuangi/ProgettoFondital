using System.Threading.Tasks;
using Fondital.Shared;
using Fondital.Shared.Repositories;
using Fondital.Data.Repositories;

namespace Fondital.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FonditalDbContext _context;
        private UtenteRepository _utenteRepository;
        private TraceRepository _traceRepository;
        private ServicePartnerRepository _servicePartnerRepository;

        public UnitOfWork(FonditalDbContext context)
        {
            this._context = context;
        }

        public IUtenteRepository Utenti => _utenteRepository ??= new UtenteRepository(_context);

        public ITraceRepository Traces => _traceRepository ??= new TraceRepository(_context);

        public IServicePartnerRepository ServicePartners => _servicePartnerRepository ??= new ServicePartnerRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}