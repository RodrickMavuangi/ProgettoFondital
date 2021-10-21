using Fondital.Data;
using Fondital.Shared.Enums;
using Fondital.Shared.Extensions;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            var rapporto = await Db.Rapporti.Include(x => x.Utente).ThenInclude(x => x.ServicePartner).SingleOrDefaultAsync(x => x.Id == Id);            
            //.Include non si tira su i RapportiVociCosto, probabilmente perché nel model c'è sia l'oggetto che la chiave
            //il codice seguente è un workaround
            rapporto.RapportiVociCosto = await Db.RapportiVociCosto.Where(x => x.RapportoId == rapporto.Id).ToListAsync();
            List<VoceCosto> voci = await Db.VociCosto.Where(x => rapporto.RapportiVociCosto.Select(y => y.VoceCostoId).Contains(x.Id)).ToListAsync();
            foreach (var rvc in rapporto.RapportiVociCosto)
                rvc.VoceCosto = voci.Single(x => x.Id == rvc.VoceCostoId);

            return rapporto;
        }

        public async Task AddRapporto(Rapporto rapporto)
        {
            await Db.Rapporti.AddAsync(rapporto);
            Db.Entry(rapporto.Utente).State = EntityState.Unchanged;
        }

        public async Task AddAudit(Rapporto rapporto, Utente utente, StatoRapporto? stato = null, string note = null)
        {
            AuditRapporto Audit = new() { Rapporto = rapporto, Utente = utente, StatoIniziale = rapporto.Stato};
            Audit.Note = note ?? (stato == null ? "Modifica campi" : $"Passaggio allo stato {stato.Description()}");

            await Db.AuditRapporti.AddAsync(Audit);
        }
    }
}