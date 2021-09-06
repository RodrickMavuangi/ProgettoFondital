using Fondital.Shared.Models;
using System.Threading.Tasks;


namespace Fondital.Shared.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}