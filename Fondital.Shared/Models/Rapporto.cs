using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using System;

namespace Fondital.Shared.Models
{
    public class Rapporto
    {
        public ServicePartnerDto ServicePartner { get; set; }
        public UtenteDto Utente { get; set; }
        public StatoRapporto Stato { get; set; }
        public DateTime Data { get; set; }
        public string Cliente { get; set; }
        public int Id { get; set; }
        public string Matricola { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
