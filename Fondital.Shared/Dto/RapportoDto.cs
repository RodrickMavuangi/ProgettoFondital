using Fondital.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Fondital.Shared.Dto
{
    public class RapportoDto
    {
        // Dati Generali
        public int Id { get; set; }
        public UtenteDto Utente { get; set; } = new UtenteDto();
        public StatoRapporto Stato { get; set; }
        public List<VoceCostoDto> VociDiCosto { get; set; } = new List<VoceCostoDto>();
        public List<RicambioDto> Ricambi { get; set; } = new List<RicambioDto>();
        public DateTime DataRapporto { get; set; }

        // Dati Anagrafici
        public ClienteDto Cliente { get; set; } = new ClienteDto();

        // Dati Caldaia
        public CaldaiaDto Caldaia { get; set; } = new CaldaiaDto();

        // Dati Intervento
        public DateTime DataIntervento { get; set; }
        public string MotivoIntervento { get; set; }
        public string TipoLavoro { get; set; }
    }
}
