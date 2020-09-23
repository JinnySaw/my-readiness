using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.EmailServiceInfoDtos
{
    public class EmailServiceForUpdateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string EmailPassword { get; set; }
        public string EmailProvider { get; set; }
        public string SmtpServer { get; set; }
        public string PortNumber { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public EmailServiceForUpdateDto()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}
