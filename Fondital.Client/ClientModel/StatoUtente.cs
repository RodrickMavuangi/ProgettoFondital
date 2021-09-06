using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.ClientModel
{
	public class StatoUtente
	{
		public bool Tutti { get; set; } = false;
		public bool Abilitati { get; set; } = false;
		public bool Disabilitati { get; set; } = false;
	}
}
