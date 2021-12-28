using Fondital.Server.ChainOfResponsability.Exceptions;
using Fondital.Shared.Models;
using System;


namespace Fondital.Server.ChainOfResponsability
{
	public class CheckPeriodoGaranzia : AbstractHandlerCheck
	{
		private int _durataGaranziaInGiorni;
		public CheckPeriodoGaranzia(int durataGaranziaInGiorni) 
		{
			_durataGaranziaInGiorni = durataGaranziaInGiorni;
		}

		public override object HandleRequest(Rapporto rapporto)
		{
			DateTime DataFineGaranzia = (DateTime)rapporto?.Caldaia?.DataMontaggio.Value.AddDays(_durataGaranziaInGiorni); 

			if (rapporto.DataIntervento > DataFineGaranzia)
			{
				throw new InterventoFuoriPeriodoGaranziaException();
			}
			
			return base.HandleRequest(rapporto);
		}
	}
}
