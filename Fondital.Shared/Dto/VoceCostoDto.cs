using Fondital.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class VoceCostoDto
    {
        public int Id { get; set; }

        [Display(Name = "NomeItaliano", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string NomeItaliano { get; set; } = "";

        [Display(Name = "NomeRusso", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string NomeRusso { get; set; } = "";
        public TipologiaVoceCosto Tipologia { get; set; } = TipologiaVoceCosto.Forfettario;
        public bool IsAbilitato { get; set; } = true;
        public List<ListinoDto> Listini { get; set; } = new List<ListinoDto>();
    }
}
