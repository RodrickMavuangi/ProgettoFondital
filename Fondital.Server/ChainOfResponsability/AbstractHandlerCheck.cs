using Fondital.Shared.Models;


namespace Fondital.Server.ChainOfResponsability
{
    // Una classe Astratta che implementa l'interfaccia Handler e Definisce i metodi setNext() e HandleRequest() dichiarati nell'interfaccia
	public abstract class AbstractHandlerCheck : IHandlerCheck
	{
        private IHandlerCheck _nextHandler;

        public IHandlerCheck SetNext(IHandlerCheck handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        // Assegna la responsabilità della gestione della richiesta al nodo successivo ...
        public virtual object HandleRequest(Rapporto rapporto)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.HandleRequest(rapporto);
            }
            else
            {
                return null;
            }
        }
    }
}
