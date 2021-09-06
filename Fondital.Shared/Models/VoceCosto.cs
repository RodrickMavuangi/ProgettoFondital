using Fondital.Shared.Enums;
using Fondital.Shared.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Models
{
    public class VoceCosto
    {
        public int Id { get; set; }
        [Required]
        public string NomeItaliano { get; set; } = "";
        [Required]
        public string NomeRusso { get; set; } = "";
        public TipologiaVoceCosto Tipologia { get; set; } = TipologiaVoceCosto.Forfettario;
        public bool IsAbilitato { get; set; } = true;
        public List<Listino> Listini { get; set; } = new List<Listino>();
    }
}
