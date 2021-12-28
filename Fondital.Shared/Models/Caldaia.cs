using System;

namespace Fondital.Shared.Models
{
    public class Caldaia
    {
        public string Matricola { get; set; }
        public string Versione { get; set; }
        public DateTime? DataVendita { get; set; }
        public DateTime? DataMontaggio { get; set; }
        public DateTime? DataAvvio { get; set; }
        public string TecnicoPrimoAvvio { get; set; }
        public int? NumCertificatoTecnico { get; set; }
        public string DittaPrimoAvvio { get; set; }

        public Brand Brand { get; set; } = new();
        public Group Group { get; set; } = new();
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime? ManufacturingDate { get; set; }
    }
}