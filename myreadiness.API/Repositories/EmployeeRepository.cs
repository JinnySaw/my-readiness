using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using myreadiness.API.Models;

namespace myreadiness.API.Data {
    public class EmployeeRepository : IEmployeeRepository {
        private readonly DataContext _context;
        public EmployeeRepository (DataContext context) {
            _context = context;
        }
        public async Task<Employee> GetEmployee (Guid id) {
            // var employee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id);
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }

        public async Task<Employee> GetEmployeeByName(string employeeName)
        {
             var employee = await _context.Employee.FirstOrDefaultAsync(e => e.EmpName == employeeName);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployees () {
            var employees = await _context.Employee.Where(e => e.IsActive == true).ToListAsync();
            return employees;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesNotInUsers () {
            var employees = await (from emp in  _context.Employee
                where !_context.Users.Any(u => u.EmpId == emp.Id) && emp.IsActive == true
                select emp).ToListAsync();
            return employees;
        }
    }
}