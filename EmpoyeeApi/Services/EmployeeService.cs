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
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == employee.DepartmentId);
            if (department == null) { return employee; }
            //Create new Employee object
            var newEmp = new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                Department = department
            };
            await _context.Employees.AddAsync(newEmp);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(Guid id)
        {
           
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == employee.DepartmentId);
#pragma warning disable CS8603 // Possible null reference return.
            if (employee == null) return null;
#pragma warning restore CS8603 // Possible null reference return.

            department.Employees.Remove(employee);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(); return employee;
        }

        public void UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

    }
}
