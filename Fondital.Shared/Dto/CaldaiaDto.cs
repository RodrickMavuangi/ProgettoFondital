using System;
using System.Globalization;

namespace Fondital.Shared.Dto
{
    public class CaldaiaDto
    {
        public string Matricola { get; set; }
        public string Versione { get; set; }
        public DateTime? DataVendita { get; set; }
        public DateTime? DataMontaggio { get; set; }
        public DateTime? DataAvvio { get; set; }
        public string TecnicoPrimoAvvio { get; set; }
        public int? NumCertificatoTecnico { get; set; }
        public string DittaPrimoAvvio { get; set; }

        public BrandDto Brand { get; set; } = new();
        public GroupDto Group { get; set; } = new();
        public string Manufacturer { get; set; } = "Fondital OOO";
        public string Model { get; set; }
        public DateTime? ManufacturingDate { get; set; } = DateTime.ParseExact("20191231", "yyyyMMdd", CultureInfo.InvariantCulture);       
    }
}