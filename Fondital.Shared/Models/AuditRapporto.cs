using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using System;

namespace Fondital.Shared.Models
{
    public class AuditRapporto
    {
        public int Id { get; set; }
        public Utente Utente { get; set; }
        public Rapporto Rapporto { get; set; }
        public StatoRapporto StatoIniziale { get; set; } = StatoRapporto.Aperto;
        public DateTime DataIntervento { get; set; } = DateTime.Now;
        public string Note { get; set; }
    }
}