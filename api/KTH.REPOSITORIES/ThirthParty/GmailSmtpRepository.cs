using System;
using KTH.MODELS.ThirdParty.SMSMKT;
using KTH.MODELS.ThirdParty.SMTP;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace KTH.REPOSITORIES.ThirthParty
{
    public interface IGmailSmtpRepository
    {
        public Task<bool> AlertSaleAccountCreate(SmtpSendModel param);

    }
    public class GmailSmtpRepository : IGmailSmtpRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string _smtpServer;
        private readonly string _smtpPort;
        private readonly string _smtpAccount;
        private readonly string _smtpPassword;



        public GmailSmtpRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _smtpServer = _configuration["SMTP:SERVER"];
            _smtpPort = _configuration["SMTP:PORT"];
            _smtpAccount = _configuration["SMTP:ACCOUNT"];
            _smtpPassword = _configuration["SMTP:PASSWORD"];

        }

        public async Task<bool> AlertSaleAccountCreate(SmtpSendModel param)
        {
            bool result = false;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", _smtpAccount.ToString()));
            message.To.Add(new MailboxAddress("Recipient Name", param.To));
            message.Subject = param.Subject;

            var body = "test";

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_smtpServer, Convert.ToInt32(_smtpPort), MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(_smtpAccount, _smtpPassword);
                    client.Send(message);

                    result = true;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }

            return result;
        }
    }
}

