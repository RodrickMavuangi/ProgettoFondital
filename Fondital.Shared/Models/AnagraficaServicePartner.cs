using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Models
{
	public class AnagraficaServicePartner
	{
		public AnagraficaServicePartner()
		{

		}
		[Editable(false)]
		public string ID { get; set; }
		[Required]
		public string CodiceFornitore { get; set; }
		[Required]
		public string RagioneSociale { get; set; }
		[Required]
		public string CodiceCliente { get; set; }
		[Editable(false)]
		[Required]
		public int NumeroUtenti { get; set; }
	}
}
