using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fondital.Shared
{
    [Table("Utente")]
    public class Utente
    {
        public int id { get; set; }
        public int SP_id { get; set; }
        public bool isAbilitato { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string username { get; set; }
        public byte[] password { get; set; }
        public DateTime pw_lastChanged { get; set; }
        public bool pw_mustChange { get; set; }
    }
}
