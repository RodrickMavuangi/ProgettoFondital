using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Repositories;
using System.Linq;

namespace Fondital.Data.Repositories
{
    public class ConfigurazioneRepository : Repository<Configurazione>, IConfigurazioneRepository
    {
        public ConfigurazioneRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext _db
        {
            get { return Context as FonditalDbContext; }
        }
    }
}