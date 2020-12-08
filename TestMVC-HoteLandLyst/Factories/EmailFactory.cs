using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.EmailConfirmation;

namespace TestMVC_HoteLandLyst.Factories
{
    public class EmailFactory
    {
        public EmailSender GetEmailSender(MailMessage message, SmtpClient client, MailAddress to, MailAddress from)
        {
            return new EmailSender(message, client, to, from);
        }

        public MailMessage GetMailMessage(MailAddress to, MailAddress from)
        {
            return new MailMessage(to, from);
        }

        public SmtpClient GetSmtpClient()
        {
            return new SmtpClient();
        }

        public MailAddress GetMailAddress(string address)
        {
            return new MailAddress(address);
        }
    }
}
