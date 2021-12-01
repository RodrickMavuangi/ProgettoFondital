using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Fondital.Shared.Dto
{
    public class ServicePartnerDto
    {
        public int Id { get; set; }

        [Display(Name = "CodiceFornitore", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string CodiceFornitore { get; set; } /*= "";*/

        [Display(Name = "RagioneSociale", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string RagioneSociale { get; set; } /*= "";*/

        [Display(Name = "CodiceCliente", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string CodiceCliente { get; set; } /*= "";*/

        public string Name { get; set; } /*= "Fornitore SRL";*/
        public string Street { get; set; } /*= "Via Europa";*/
        public string HouseNr { get; set; } /*= "184";*/
        public string City { get; set; } /*= "Roma";*/
        public string Region { get; set; } /*= "Lazio";*/
        public string PostalCode { get; set; } /*= "00144";*/
        public string Phone { get; set; } /*= "067485596";*/
        public string Email { get; set; } /*= "fornitore@ffo.kr";*/
        public string StateRegistrationNr { get; set; } /*= "01748";*/
        public string INN { get; set; } /*= "ABCDEF00A01G482A";*/
        public string KPP { get; set; } /*= "2889445";*/
        public string CC { get; set; } /*= "IT000000528224785";*/
        public string BankName { get; set; } /*= "Mediabanca";*/
        public string CCC { get; set; } /*= "IT000000000224785";*/
        public string BankCode { get; set; } /*= "224785";*/
        public string ManagerName { get; set; } /*= "Antonio Ferrari";*/
        public string ContractNr { get; set; } /*= "4723";*/
        public DateTime? ContractDate { get; set; } /*= DateTime.ParseExact("20200305", "yyyyMMdd", CultureInfo.InvariantCulture);*/

        public List<UtenteDto> Utenti { get; set; } = new List<UtenteDto>();
        public List<ListinoDto> Listini { get; set; } = new List<ListinoDto>();
    }
}