using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared
{
    [Table("Trace")]
    public class Trace
    {
        public int id { get; set; }
        
        [Column(TypeName = "varchar(255)")]
        public TraceType tipologia { get; set; }

        public string descrizione { get; set; }
        public int utente_id { get; set; }
        public int rapportino_id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime eventDateTime { get; set; }
    }

    public enum TraceType
    {
        LoginInfo = 0,
        WorkflowRapportino,
        AzioniDirezione
            //TODO: aggiungere qualora necessario
    }
}
