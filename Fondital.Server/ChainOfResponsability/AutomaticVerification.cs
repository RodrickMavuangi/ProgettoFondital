using Fondital.Server.ChainOfResponsability.Exceptions;
using Fondital.Shared.Enums;
using Fondital.Shared.Models;


namespace Fondital.Server.ChainOfResponsability
{
	public class AutomaticVerification
	{
		public StatoRapporto CheckAndUpdateStatus(Rapporto rapporto, int DurataGaranziaInGiorni)
		{
			StatoRapporto result = new StatoRapporto();
			var CheckPeriodoGaranzia = new CheckPeriodoGaranzia(DurataGaranziaInGiorni);
			var CheckVociDiCosto = new CheckVociDiCosto();
			CheckPeriodoGaranzia.SetNext(CheckVociDiCosto);

			try
			{
				var response = CheckPeriodoGaranzia.HandleRequest(rapporto);

				// response = null -> Abbiamo percorso tutti i nodi della catena e abbiamo fatto tutti i controlli senza trovare nessun errore
				if (response == null)
					result = StatoRapporto.Approvato;
			}
			catch(InterventoFuoriPeriodoGaranziaException e)
			{
				result = StatoRapporto.DaVerificare;
			}
			catch(VoceCostoTipologiaForfettariaException e)
			{
				result = StatoRapporto.DaVerificare;
			}
			
			return result;
		}
	}
}
