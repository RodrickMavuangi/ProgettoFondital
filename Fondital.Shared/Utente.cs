using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fondital.Shared
{
    [Table("Utente")]
    public class Utente
    {
        public int Id { get; set; }
        public int SP_Id { get; set; }
        public bool IsAbilitato { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public DateTime Pw_LastChanged { get; set; }
        public bool Pw_MustChange { get; set; }
    }
}
