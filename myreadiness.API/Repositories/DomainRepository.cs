using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myreadiness.API.Models;
using Microsoft.EntityFrameworkCore;

namespace myreadiness.API.Data
{
    public class DomainRepository : IDomainRepository
    {
        private readonly DataContext _context;

        public DomainRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Delete()
        {
            var list = await _context.Domain.ToListAsync();
            list.ForEach(a => a.IsDeleted = true);
        }

        public async Task<Domain> GetActiveDomain()
        {
            var obj = await _context.Domain.FirstOrDefaultAsync(l => l.IsActive == true && l.IsDeleted == false);
            return obj;
        }

        public async Task<Domain> GetDomain(Guid id)
        {
            var obj = await _context.Domain.FirstOrDefaultAsync(l => l.ID == id);
            return obj;
        }

        public async Task<IEnumerable<Domain>> GetDomains()
        {
            var obj = await _context.Domain.ToListAsync();
            return obj;
        }
    }
}