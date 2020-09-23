using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myreadiness.API.Models;

namespace myreadiness.API.Data
{
    public interface IEmailServiceRepository
    {
        Task<IEnumerable<EmailServiceInfo>> GetEmailServiceInfoes();
        Task<EmailServiceInfo> GetEmailServiceInfo(Guid id);
        Task<EmailServiceInfo> GetActiveEmailServiceInfo();
        Task Delete(); 
    }
}
