namespace Fondital.Shared.Dto
{
    public class RicambioDto
    {
        public int Id { get; set; }
        public int Quantita { get; set; }
        public int Costo { get; set; }
        public string Descrizione { get; set; }
        public RapportoDto Rapporto { get; set; }
    }
}
