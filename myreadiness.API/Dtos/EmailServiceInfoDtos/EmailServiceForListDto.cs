using System;
namespace myreadiness.API.Dtos.EmailServiceInfoDtos
{
    public class EmailServiceForListDto
    {
        public Guid Id { get; set; }
        public string SenderName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailProvider { get; set; }
        public string SmtpServer { get; set; }
        public string PortNumber { get; set; }
    }
}
