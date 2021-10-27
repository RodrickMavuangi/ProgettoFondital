using Fondital.Shared.Dto;

namespace Fondital.Shared.Services
{
    public interface IMailService
    {
        void SendEmail(MailRequestDto mailRequest);
    }
}