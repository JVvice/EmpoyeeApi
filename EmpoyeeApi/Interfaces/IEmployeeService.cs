﻿using EmpoyeeApi.Models;

namespace EmpoyeeApi.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<Employee>AddEmployeeAsync(Employee employee);
        Task<Employee> DeleteEmployeeAsync(Guid id);
        void UpdateEmployeeAsync(Employee employee);
    }
}
