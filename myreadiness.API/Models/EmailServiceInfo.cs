using System;
namespace myreadiness.API.Models
{
    public class EmailServiceInfo
    {
        public Guid Id { get; set; }
        public string SenderName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string EmailProvider { get; set; }
        public string SmtpServer { get; set; }
        public string PortNumber { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
