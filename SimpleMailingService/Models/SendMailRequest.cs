using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleMailingService.Attributes;
using SimpleMailingService.Enums;

namespace SimpleMailingService.Models
{
    public class SendMailRequest
    {
        [Required(ErrorMessage = "Recipient is a required field."), AnyRequired(ErrorMessage = "At least one recipient is required.")]
        public List<Recipient> Recipients { get; set; }

        [Required(ErrorMessage = "Subject is a required field.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Body is a required field.")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Client is a required field.")]
        public ClientEnum Client { get; set; } = ClientEnum.Default;

        public List<Attachment> Attachments { get; set; }
    }

    public class Recipient
    {
        [Required(ErrorMessage = "Recipient.Email is a required field."), EmailAddress(ErrorMessage = "Recipient.Email should be in email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Recipient.Name is a required field.")]
        public string Name { get; set; }
    }

    public class Attachment
    {
        [Required(ErrorMessage = "Attachment.Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Attachment.MimeType is a required field.")]
        public MimeType MimeType { get; set; }

        [Required(ErrorMessage = "Attachment.Base64Content is a required field.")]
        public byte[] Base64Content { get; set; }
    }

    public class MimeType
    {
        [Required(ErrorMessage = "MimeType.Base is a required field.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "MimeType.Sub is a required field.")]
        public string SubType { get; set; }
    }
}
