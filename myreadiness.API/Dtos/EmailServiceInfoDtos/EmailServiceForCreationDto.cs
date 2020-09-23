using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.EmailServiceInfoDtos
{
    public class EmailServiceForCreationDto
    {
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public EmailServiceForCreationDto()
        {
            IsActive = true;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}
