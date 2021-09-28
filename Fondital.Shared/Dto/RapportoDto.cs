using Fondital.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Fondital.Shared.Dto
{
    public class RapportoDto
    {
        // Dati Generali
        public int Id { get; set; }
        public UtenteDto Utente { get; set; } = new();
        public StatoRapporto Stato { get; set; } = 0;
        public List<RapportoVoceCostoDto> VociDiCosto { get; set; } = new();
        public List<RicambioDto> Ricambi { get; set; } = new();
        public DateTime? DataRapporto { get; set; }
        public string NomeTecnico { get; set; }

        // Dati Anagrafici
        public ClienteDto Cliente { get; set; } = new();

        // Dati Caldaia
        public CaldaiaDto Caldaia { get; set; } = new();

        // Dati Intervento
        public DateTime? DataIntervento { get; set; }
        public string MotivoIntervento { get; set; }
        public string TipoLavoro { get; set; }
    }
}
