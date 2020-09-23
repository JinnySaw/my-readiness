using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myreadiness.API.Data;
using myreadiness.API.Models;
using Microsoft.EntityFrameworkCore;

namespace myreadiness.API.Data
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        private readonly DataContext _context;

        public EmailServiceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Delete()
        {
            var list = await _context.EmailServiceInfo.ToListAsync();
            list.ForEach(a => a.IsDeleted = true);
        }
        public async Task<EmailServiceInfo> GetActiveEmailServiceInfo()
        {
            var obj = await _context.EmailServiceInfo.FirstOrDefaultAsync(l => l.IsActive == true && l.IsDeleted == false);
            return obj;
        }

        public async Task<EmailServiceInfo> GetEmailServiceInfo(Guid id)
        {
            var obj = await _context.EmailServiceInfo.FirstOrDefaultAsync(l => l.Id == id);
            return obj;
        }

        public async Task<IEnumerable<EmailServiceInfo>> GetEmailServiceInfoes()
        {
            var obj = await _context.EmailServiceInfo.Where(e=> e.IsActive == true && e.IsDeleted == false).ToListAsync();
            return obj;
        }
    }
}
