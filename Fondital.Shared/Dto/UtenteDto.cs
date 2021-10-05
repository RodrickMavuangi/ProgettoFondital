using Fondital.Shared.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Dto
{
    public class UtenteDto : IdentityUser<int>
    {
        public ServicePartnerDto ServicePartner { get; set; }
        public bool IsAbilitato { get; set; } = true;

        [Display(Name = "Nome", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Nome { get; set; }

        [Display(Name = "Cognome", ResourceType = typeof(Resources.Display))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Validation))]
        public string Cognome { get; set; }
        public DateTime Pw_LastChanged { get; set; } = DateTime.Now;
        public bool Pw_MustChange { get; set; } = true;
        public List<RapportoDto> Rapporti { get; set; } = new List<RapportoDto>();
        public List<RuoloDto> Ruoli { get; set; } = new List<RuoloDto>();
    }
}
