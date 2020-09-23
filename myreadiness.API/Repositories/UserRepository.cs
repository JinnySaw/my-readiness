using System;
using System.Threading.Tasks;
using myreadiness.API.Data;
using myreadiness.API.Helpers;
using myreadiness.API.Models;
using Microsoft.EntityFrameworkCore;

namespace myreadiness.API.Repositories
{
     public class UserRepository : IUserRepository {
        private readonly DataContext _context;
        public UserRepository (DataContext context) {
           _context = context;
        }
        public async Task<User> GetUser (Guid id) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> GetUserByEmpId (Guid empid) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmpId == empid);
            return user;
        }

        public async Task<PagedList<User>> GetUsers (ObjectParams objParams) {
            var users =  _context.Users;
            return await PagedList<User>.CreateAsync(users, objParams.PageNumber, objParams.PageSize);
        }
    }
}