using System;

namespace Messenger.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Theme { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
    }
}
