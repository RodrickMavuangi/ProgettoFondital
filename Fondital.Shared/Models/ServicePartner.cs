using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;

namespace Fondital.Shared.Models
{
    public class ServicePartner
    {
        public int Id { get; set; }
        public string CodiceFornitore { get; set; }
        public string RagioneSociale { get; set; }
        public string CodiceCliente { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StateRegistrationNr { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string CC { get; set; }
        public string BankName { get; set; }
        public string CCC { get; set; }
        public string BankCode { get; set; }
        public string ManagerName { get; set; }
        public string ContractNr { get; set; }
        public DateTime? ContractDate { get; set; }

        public List<Utente> Utenti { get; set; } = new List<Utente>();
        public List<Listino> Listini { get; set; } = new List<Listino>();
    }
}