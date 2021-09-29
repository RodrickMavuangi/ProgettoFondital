using Fondital.Shared.Models;

namespace Fondital.Shared.Services
{
    public interface IMailService
    {
        void SendEmailAsync(MailRequest mailRequest);
    }
}