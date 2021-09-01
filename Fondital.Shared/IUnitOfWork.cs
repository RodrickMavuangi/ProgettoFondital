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
        Task<int> CommitAsync();
    }
}