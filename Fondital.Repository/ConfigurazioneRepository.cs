using Fondital.Data;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;

namespace Fondital.Repository
{
    public class ConfigurazioneRepository : Repository<Configurazione>, IConfigurazioneRepository
    {
        public ConfigurazioneRepository(FonditalDbContext context)
            : base(context)
        { }
    }
}