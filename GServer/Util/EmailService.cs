
using MimeKit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Configuration;
using System.Web;
using MailKit.Security;

namespace Gopet.Util
{
    public class EmailService
    {
        public string Email { get; }
        public string Password { get; }
        public string SmtpDomain { get; }
        public int Port { get; }



        public EmailService(string Text) : this(HttpUtility.ParseQueryString(Text.Replace(";", "&")))
        {

        }

        public EmailService(NameValueCollection appSettings)
        {
            this.Email = appSettings["Email"];
            this.Password = appSettings["Password"];
            this.SmtpDomain = appSettings["SMTP"];
            this.Port = int.Parse(appSettings["PORT"]);
        }

        public EmailService(string email, string password, string smtpDomain, int port)
        {
            this.Email = email;
            this.Password = password;
            this.SmtpDomain = smtpDomain;
            this.Port = port;
        }

        public void SendEmail(string to, string subject, string body, string type = "plain")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bot", Email));
            message.To.Add(new MailboxAddress(string.Empty, to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect(this.SmtpDomain, this.Port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(this.Email, this.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public async void SendEmailAsync(string to, string subject, string body, string type = "plain")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bot", Email));
            message.To.Add(new MailboxAddress(string.Empty, to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(this.SmtpDomain, this.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(this.Email, this.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
