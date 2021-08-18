using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Models
{
    [Table("Trace")]
    public class Trace
    {
        public int Id { get; set; }
        public TraceType Tipologia { get; set; }
        public string Descrizione { get; set; }
        public Utente Utente { get; set; }
        public int Rapportino_Id { get; set; }
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
