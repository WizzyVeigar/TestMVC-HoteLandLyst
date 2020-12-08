using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Factories;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.EmailConfirmation
{
    public class EmailSender : ISendEmail
    {
        private MailMessage message;

        public MailMessage Message
        {
            get { return message; }
            private set { message = value; }
        }

        private SmtpClient smtp;

        public SmtpClient Smtp
        {
            get { return smtp; }
            private set { smtp = value; }
        }

        private MailAddress to;

        public MailAddress To
        {
            get { return to; }
            private set { to = value; }
        }

        private MailAddress from;


        public MailAddress From
        {
            get { return from; }
            private set { from = value; }
        }
        public EmailSender(MailMessage message, SmtpClient smtpClient, MailAddress to, MailAddress from)
        {
            Message = message;
            Smtp = smtpClient;
            To = to;
            From = from;
        }


        public void SendEmail()
        {
            try
            {
                message.IsBodyHtml = false;
                smtp.Port = 587;
                smtp.Host = "smtp.live.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("HotelLandlyst", "password"); //Is this okay?
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
