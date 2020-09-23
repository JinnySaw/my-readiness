using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myreadiness.API.Models;

namespace myreadiness.API.Data
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> GetEmployeesNotInUsers();
        Task<Employee> GetEmployee(Guid id);
        Task<Employee> GetEmployeeByName(string employeeName);
    }
}