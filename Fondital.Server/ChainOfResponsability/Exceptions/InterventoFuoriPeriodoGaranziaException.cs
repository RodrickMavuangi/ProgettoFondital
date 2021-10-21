using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Server.ChainOfResponsability.Exceptions
{
	public class InterventoFuoriPeriodoGaranziaException : Exception
	{
        public override string Message
        {
            get
            {
                return "L'intervento non è fatto entro il Periodo di Garanzia";
            }
        }
    }
}
