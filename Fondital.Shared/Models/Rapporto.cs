using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;

namespace Fondital.Shared.Models
{
    public class Rapporto
    {
        // Dati Generali
        public int Id { get; set; }
        public Utente Utente { get; set; }
        public StatoRapporto Stato { get; set; }
        public List<Ricambio> Ricambi { get; set; }
        public List<VoceCosto> VociDiCosto { get; set; }
        public DateTime DataRapporto { get; set; }

        // Dati Anagrafici Cliente
        public Cliente Cliente { get; set; } = new Cliente();

        // Dati Caldaia
        public Caldaia Caldaia { get; set; } = new Caldaia();

        // Dati Intervento
        public DateTime DataIntervento { get; set; }
        public string MotivoIntervento { get; set; }
        public string TipoLavoro { get; set; }
    }
}
