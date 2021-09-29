namespace Fondital.Shared.Dto
{
    public class RapportoVoceCostoDto
    {
        public int RapportoId { get; set; }
        public RapportoDto Rapporto { get; set; } = new();
        public int VoceCostoId { get; set; }
        public VoceCostoDto VoceCosto { get; set; } = new();
        public int Quantita { get; set; }
    }
}
