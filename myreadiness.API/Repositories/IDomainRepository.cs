using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myreadiness.API.Models;

namespace myreadiness.API.Data
{
    public interface IDomainRepository
    {
        Task<IEnumerable<Domain>> GetDomains();
        Task<Domain> GetDomain(Guid id);
        Task<Domain> GetActiveDomain();
        Task Delete();
    }
}