namespace Fondital.Shared.Models
{
    public class Ricambio
    {
        public int Id { get; set; }
        public int Quantita { get; set; }
        public int Costo { get; set; }
        public string Descrizione { get; set; }
        public Rapporto Rapporto { get; set; }
    }
}
