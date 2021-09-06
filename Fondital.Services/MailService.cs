using Fondital.Shared.Models;
using Fondital.Shared.Models.Settings;
using Fondital.Shared.Services;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MimeKit;
//using MailKit.Net.Smtp;
using System.Net.Mail;
using MailKit.Security;
using System;

namespace Fondital.Services
{
    public class MailService : IMailService
    {

        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
			//try
			//{
			//	var email = new MimeMessage();
			//	email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
			//	email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
			//	email.Subject = mailRequest.Subject;
			//	var builder = new BodyBuilder();
			//	//if (mailRequest.Attachments != null)
			//	//{
			//	//	byte[] fileBytes;
			//	//	foreach (var file in mailRequest.Attachments)
			//	//	{
			//	//		if (file.Length > 0)
			//	//		{
			//	//			using (var ms = new MemoryStream())
			//	//			{
			//	//				file.CopyTo(ms);
			//	//				fileBytes = ms.ToArray();
			//	//			}
			//	//			builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
			//	//		}
			//	//	}
			//	//}
			//	builder.HtmlBody = mailRequest.Body;
			//	email.Body = builder.ToMessageBody();
			//	using var smtp = new SmtpClient();
			//	smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.None);
			//	//smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
			//	await smtp.SendAsync(email);
			//	smtp.Disconnect(true);
			//}
			//catch (Exception e)
			//{

			//}

			try
			{
				MailMessage msg = new MailMessage();
				msg.To.Add(new MailAddress(mailRequest.ToEmail));
				msg.From = new MailAddress(_mailSettings.Mail, "You");
				msg.Subject = "This is a Test Mail";
				msg.Body = "This is a test message using Exchange OnLine";
				msg.IsBodyHtml = true;

				SmtpClient client = new SmtpClient();
				client.UseDefaultCredentials = false;
				client.Credentials = new System.Net.NetworkCredential(_mailSettings.DisplayName,_mailSettings.Password);
				client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
				client.Host = _mailSettings.Host;// "smtp.office365.com";
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.EnableSsl = true;
				try
				{
					client.Send(msg);
					 //"Message Sent Succesfully";
				}
				catch (Exception ex)
				{
					var res = ex.ToString();
				}
			}
			catch(Exception e)
			{
				var res1 = e.ToString();
			}

			

		}
    }
}
