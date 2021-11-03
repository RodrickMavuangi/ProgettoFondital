namespace Fondital.Shared.Models
{
    public class Ricambio
    {
        public int Id { get; set; }
        public int Quantita { get; set; }

        public string Code { get; set; }
        public string RUDescription { get; set; }
        public string ITDescription { get; set; }
        public decimal Costo { get; set; } //amount

        public Rapporto Rapporto { get; set; }
    }
}