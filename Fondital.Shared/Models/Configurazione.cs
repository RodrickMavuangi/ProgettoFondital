using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Models
{
    [Table("Configurazioni")]
    public class Configurazione
    {
        public int Id { get; set; }
        public string Chiave { get; set; }

        //Valore non è di tipo DurataValidita ma stringa per rendere la classe usabile anche per altre configurazioni
        public string Valore { get; set; }
    }
}
