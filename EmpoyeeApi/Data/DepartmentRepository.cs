using EmpoyeeApi.Models;
using EmpoyeeApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeApi.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            _context.SaveChanges();
            return department;

        }
    }
}
