using System;
using System.ComponentModel.DataAnnotations;

namespace Fondital.Shared.Models
{
    public class Listino
    {
        public int Id { get; set; }
        public ServicePartner ServicePartner { get; set; }
        public VoceCosto VoceCosto { get; set; }
        public string Raggruppamento { get; set; } = "";
        public int Valore { get; set; } = 0;
    }
}
