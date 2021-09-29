using Fondital.Shared.Models.Auth;
using System.Collections.Generic;

namespace Fondital.Shared.Models
{
    public class ServicePartner
    {
        public int Id { get; set; }
        public string CodiceFornitore { get; set; }
        public string RagioneSociale { get; set; }
        public string CodiceCliente { get; set; }
        public List<Utente> Utenti { get; set; } = new List<Utente>();
        public List<Listino> Listini { get; set; } = new List<Listino>();
    }
}
