using Fondital.Shared.Models;


namespace Fondital.Server.ChainOfResponsability
{
    public interface IHandlerCheck
    {
        // imposta il nodo successivo che deve gestire la richiesta
        IHandlerCheck SetNext(IHandlerCheck handler);
        
        object HandleRequest(Rapporto rapporto);
    }
}
