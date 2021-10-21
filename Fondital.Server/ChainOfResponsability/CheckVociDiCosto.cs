using Fondital.Server.ChainOfResponsability.Exceptions;
using Fondital.Shared.Models;


namespace Fondital.Server.ChainOfResponsability
{
	public class CheckVociDiCosto : AbstractHandlerCheck
	{
		public override object HandleRequest(Rapporto rapporto)
		{
			foreach (var item in rapporto.RapportiVociCosto)
			{
				if (item.VoceCosto.Tipologia == Shared.Enums.TipologiaVoceCosto.Forfettario)
					throw new VoceCostoTipologiaForfettariaException();
			}

			return base.HandleRequest(rapporto);
		}
	}
}
