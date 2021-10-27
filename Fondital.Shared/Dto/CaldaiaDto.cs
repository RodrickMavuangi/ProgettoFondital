using System;

namespace Fondital.Shared.Dto
{
    public class CaldaiaDto
    {
        public string Matricola { get; set; }
        public BrandDto Brand { get; set; }
        public GroupDto Group { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public string Versione { get; set; }
        public DateTime? DataVendita { get; set; }
        public DateTime? DataMontaggio { get; set; }
        public DateTime? DataAvvio { get; set; }
        public string TecnicoPrimoAvvio { get; set; }
        public int? NumCertificatoTecnico { get; set; }
        public string DittaPrimoAvvio { get; set; }
    }
}
