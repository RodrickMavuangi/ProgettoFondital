namespace Fondital.Shared.Dto
{
    public class RicambioDto
    {
        public int Id { get; set; }
        public int Quantita { get; set; } = 1;

        public string Code { get; set; } = "01556";
        public string ITDescription { get; set; } = "Ricambio IT";
        public string RUDescription { get; set; } = "Ricambio RU";
        public decimal Costo { get; set; } = 10; //amount

        public RapportoDto Rapporto { get; set; }
    }
}