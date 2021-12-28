namespace Fondital.Shared.Models
{
    public class RapportoVoceCosto
    {
        public int RapportoId { get; set; }
        public Rapporto Rapporto { get; set; } = new();
        public int VoceCostoId { get; set; }
        public VoceCosto VoceCosto { get; set; } = new();
        public int Quantita { get; set; }
    }
}
