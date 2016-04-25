using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Skunkworks.Ics.Web.Managers
{
    public class EmailManager : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            const string credentialUserName = "colinthomas";
            const string sentFrom = "GoodTyms <no-reply@goodtyms.com>";
            const string pwd = "kundaraRamona1";

            // Configure the client:
            var client = new SmtpClient("smtp.sendgrid.net")
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };


            // Create the credentials:
            var credentials = new NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;


            // Create the message:
            var mail = new MailMessage(sentFrom, message.Destination)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };


            // Send:
            return client.SendMailAsync(mail);
        }
    }
}