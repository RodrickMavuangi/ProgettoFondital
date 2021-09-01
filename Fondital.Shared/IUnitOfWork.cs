using System;
using System.Threading.Tasks;
using Fondital.Shared.Repositories;

namespace Fondital.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        IUtenteRepository Utenti { get; }
        ITraceRepository Traces { get; }
        IServicePartnerRepository ServicePartners { get; }
        IConfigurazioneRepository Configurazioni { get; }
        IDifettoRepository Difetti { get; }
        Task<int> CommitAsync();
        void Update<T>(T Old, T New) where T : class;
    }
}