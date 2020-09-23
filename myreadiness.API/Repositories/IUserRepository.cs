using System;
using System.Threading.Tasks;
using myreadiness.API.Helpers;
using myreadiness.API.Models;

namespace myreadiness.API.Repositories
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetUsers(ObjectParams objParams);
        Task<User> GetUser(Guid id);
        Task<User> GetUserByEmpId(Guid empid);
    }
}