using System.ComponentModel;

namespace Fondital.Shared.Enums
{
    public enum StatoRapporto
    {
        Aperto = 0,
        Registrato,
        [Description("Da Verificare")]
        DaVerificare,
        Rifiutato,
        Approvato,
        Validato
    }
}