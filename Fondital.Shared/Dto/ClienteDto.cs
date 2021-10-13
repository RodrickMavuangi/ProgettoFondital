namespace Fondital.Shared.Dto
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }
        public string NumCivico { get; set; }
        public string NumTelefono { get; set; }
        public string Email { get; set; }
        public string FullName => $"{Nome} {Cognome}";
    }
}
