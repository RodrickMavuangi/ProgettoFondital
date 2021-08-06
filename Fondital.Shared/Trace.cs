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
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(255)")]
        public TraceType Tipologia { get; set; }

        public string Descrizione { get; set; }
        public int Utente_Id { get; set; }
        public int Rapportino_Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime EventDateTime { get; set; }
    }

    public enum TraceType
    {
        LoginInfo = 0,
        WorkflowRapportino,
        AzioniDirezione
            //TODO: aggiungere qualora necessario
    }
}
