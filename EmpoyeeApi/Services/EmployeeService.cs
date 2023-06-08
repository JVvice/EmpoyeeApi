using EmpoyeeApi.Data;
using EmpoyeeApi.Interfaces;
using EmpoyeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee>GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            return employee;
        }

        public async Task<Employee>AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            if (employee == null) return null;
#pragma warning restore CS8603 // Possible null reference return.

            _context.Employees.Remove(employee);
            _context.SaveChanges(); return employee;
        }

    }
}
