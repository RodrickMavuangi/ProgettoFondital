using Fondital.Shared.Repositories;
using System;
using System.Threading.Tasks;

namespace Fondital.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        IUtenteRepository Utenti { get; }
        IServicePartnerRepository ServicePartners { get; }
        IConfigurazioneRepository Configurazioni { get; }
        IDifettoRepository Difetti { get; }
        IVoceCostoRepository VociCosto { get; }
        IListinoRepository Listini { get; }
        ILavorazioneRepository Lavorazioni { get; }
        IRapportoRepository Rapporti { get; }
        IRuoloRepository Roles { get; }
        IUserRolesRepository UserRoles { get; }
        Task<int> CommitAsync();
        void Update<T>(T Old, T New) where T : class;
    }
}