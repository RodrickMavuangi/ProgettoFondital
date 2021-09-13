using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fondital.Shared.Models
{
    public class Lavorazione
    {
        public int Id { get; set; }
        [Required]
        public string NomeItaliano { get; set; } = "";
        [Required]
        public string NomeRusso { get; set; } = "";
        public bool IsAbilitato { get; set; } = true;
    }
}
