namespace Fondital.Shared.Models
{
    public class Lavorazione
    {
        public int Id { get; set; }
        public string NomeItaliano { get; set; }
        public string NomeRusso { get; set; }
        public bool IsAbilitato { get; set; } = true;
    }
}
