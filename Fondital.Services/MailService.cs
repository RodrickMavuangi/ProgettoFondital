using Fondital.Shared.Models;
using Fondital.Shared.Models.Settings;
using Fondital.Shared.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;

namespace Fondital.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public void SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(mailRequest.ToEmail));
                msg.From = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);
                msg.Subject = mailRequest.Subject;
                msg.Body = mailRequest.Body;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                client.Port = int.Parse(_mailSettings.Port);
                client.Host = _mailSettings.Host;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                client.Send(msg);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}