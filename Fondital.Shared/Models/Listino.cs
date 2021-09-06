using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Models
{
    public class Listino
    {
        public int Id { get; set; }
        [Required]
        public ServicePartner ServicePartner { get; set; }
        [Required]
        public VoceCosto VoceCosto { get; set; }
        [Required]
        public string Raggruppamento { get; set; } = "";
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Valore { get; set; } = 0;
    }
}
