using System.IO;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SimpleMailingService.Extensions;
using SimpleMailingService.Models;

namespace SimpleMailingService.Services
{
    public interface ISendService
    {
        void Send(SendMailRequest request);
    }

    public class SendService : ISendService
    {
        private readonly ClientOptions _clientOptions;
        public SendService(IOptions<ClientOptions> clientOptions)
        {
            _clientOptions = clientOptions.Value;
        }

        public void Send(SendMailRequest request)
        {
            var client = request.Client.GetClient(_clientOptions);
            var message = new MimeMessage
            {
                Subject = request.Subject
            };

            request.Recipients?.ForEach(recipient =>
            {
                message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
            });

            message.From.Add(new MailboxAddress(client.Name, client.Email));

            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart(TextFormat.Html)
            {
                Text = request.Body
            });

            request.Attachments?.ForEach(attachment =>
            {
                multipart.Add(new MimePart(attachment.MimeType.Type, attachment.MimeType.SubType)
                {
                    Content = new MimeContent(new MemoryStream(attachment.Base64Content), ContentEncoding.Default),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachment.Name
                });
            });

            message.Body = multipart;

            using var smtpClient = new SmtpClient();

            smtpClient.Connect(client.Host, client.Port, client.UseSSL);

            if(client.Authentication!=null)
               smtpClient.Authenticate(client.Authentication.Username, client.Authentication.Password);

            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}