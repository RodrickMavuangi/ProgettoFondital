using Newtonsoft.Json;

namespace Fondital.Shared.Dto
{
    public class RicambioDto
    {
        public int Id { get; set; }
        public int Quantita { get; set; }

        public string Code { get; set; }
        public string ITDescription { get; set; }
        public string RUDescription { get; set; }
        public decimal Amount { get; set; }

        public RapportoDto Rapporto { get; set; }
    }
}