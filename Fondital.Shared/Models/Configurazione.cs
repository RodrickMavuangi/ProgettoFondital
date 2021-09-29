namespace Fondital.Shared.Models
{
    public class Configurazione
    {
        public int Id { get; set; }
        public string Chiave { get; set; }

        //Valore non è di tipo DurataValidita ma stringa per rendere la classe usabile anche per altre configurazioni
        public string Valore { get; set; }
    }
}
