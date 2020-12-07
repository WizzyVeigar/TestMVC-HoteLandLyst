using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Entities
{
    public class Email
    {
        private string sender;

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private string receiver;

        public string Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        public Email(string to, string from, string body, string subject)
        {
            Receiver = to;
            Sender = from;
            Body = body;
            Subject = subject;
        }

        private Email()
        {
            Receiver = string.Empty;
            Sender = string.Empty;
            Body = string.Empty;
            Subject = string.Empty;
        }
    }
}
