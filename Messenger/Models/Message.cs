using Messenger.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MessageConstrains.SenderMaxStringLength,
            MinimumLength = MessageConstrains.NameMinStringLength)]
        public string Sender { get; set; }

        [Required]
        [StringLength(MessageConstrains.ReceiverMaxStringLength,
            MinimumLength = MessageConstrains.NameMinStringLength)]
        public string Receiver { get; set; }

        [Required]
        [StringLength(MessageConstrains.ThemeMaxStringLength)]
        public string Theme { get; set; }

        [Required]
        [StringLength(MessageConstrains.BodyMaxStringLength)]
        public string Body { get; set; }

        [Required]
        public DateTime DateSent { get; set; }
    }
}
