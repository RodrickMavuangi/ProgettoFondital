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
            //workaround: Include per i RapportiVociCosto non funziona
            var rapporti = await Db.Rapporti.Include(x => x.Utente).ThenInclude(x => x.ServicePartner).ToListAsync();
            foreach (var rapporto in rapporti)
            {
                rapporto.RapportiVociCosto = await Db.RapportiVociCosto.Where(x => x.RapportoId == rapporto.Id).ToListAsync();
                foreach (var rvc in rapporto.RapportiVociCosto)
                {
                    rvc.Rapporto = rapporto;
                    rvc.VoceCosto = await Db.VociCosto.SingleAsync(x => x.Id == rvc.VoceCostoId);
                }
            }

            return rapporti;
        }

        public async Task<Rapporto> GetRapportoByIdAsync(int Id)
        {
            //workaround: Include per i RapportiVociCosto non funziona
            var rapporto = await Db.Rapporti.Include(x => x.Utente).ThenInclude(x => x.ServicePartner).SingleOrDefaultAsync(x => x.Id == Id);
            rapporto.RapportiVociCosto = await Db.RapportiVociCosto.Where(x => x.RapportoId == rapporto.Id).ToListAsync();

            foreach (var rvc in rapporto.RapportiVociCosto)
            {
                rvc.Rapporto = rapporto;
                rvc.VoceCosto = await Db.VociCosto.SingleAsync(x => x.Id == rvc.VoceCostoId);
            }

            try
            {
                Db.Attach(rapporto.Cliente).CurrentValues.SetValues(new Dictionary<string, object> { ["RapportoId"] = rapporto.Id });
                Db.Attach(rapporto.Caldaia).CurrentValues.SetValues(new Dictionary<string, object> { ["RapportoId"] = rapporto.Id });
                Db.Attach(rapporto.Caldaia.Brand).CurrentValues.SetValues(new Dictionary<string, object> { ["CaldaiaRapportoId"] = rapporto.Id });
                Db.Attach(rapporto.Caldaia.Group).CurrentValues.SetValues(new Dictionary<string, object> { ["CaldaiaRapportoId"] = rapporto.Id });
            }
            catch
            { }

            return rapporto;
        }

        public void EditRapportiVociCostoList(List<RapportoVoceCosto> oldList, List<RapportoVoceCosto> newList)
        {
            foreach (var rvc in oldList)
                Db.Entry(rvc).State = EntityState.Deleted;
            foreach (var rvc in newList)
                Db.Entry(rvc).State = EntityState.Added;
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