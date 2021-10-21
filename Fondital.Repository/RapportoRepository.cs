using Fondital.Data;
using Fondital.Shared.Enums;
using Fondital.Shared.Extensions;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fondital.Repository
{
    public class RapportoRepository : Repository<Rapporto>, IRapportoRepository
    {
        public RapportoRepository(FonditalDbContext context)
            : base(context)
        { }

        private FonditalDbContext Db
        {
            get { return Context as FonditalDbContext; }
        }

        public async Task<List<Rapporto>> GetAllRapporti()
        {
            return await Db.Rapporti.Include(x => x.Utente).ThenInclude(x => x.ServicePartner).ToListAsync();
        }

        public async Task<Rapporto> GetRapportoByIdAsync(int Id)
        {
            return await Db.Rapporti.Include(x => x.Utente).ThenInclude(x => x.ServicePartner).SingleOrDefaultAsync(x => x.Id == Id);
        }

        public void AddRapporto(Rapporto rapporto)
        {
            Db.Entry(rapporto).State = EntityState.Added;
        }

        public void AddAudit(Rapporto rapporto, Utente utente, StatoRapporto? stato = null, string note = null)
        {
            AuditRapporto audit = new() { Rapporto = rapporto, Utente = utente, StatoIniziale = rapporto.Stato };
            audit.Note = note ?? (stato == null ? "Modifica campi" : $"Passaggio allo stato {stato.Description()}");
            Db.Entry(audit).State = EntityState.Added;
        }
    }
}