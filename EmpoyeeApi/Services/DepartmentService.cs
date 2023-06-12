using EmpoyeeApi.Data;
using EmpoyeeApi.Interfaces;
using EmpoyeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments
                .ToListAsync();
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            _context.SaveChanges();
            return department;

        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            var department = await _context.Departments
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.DepartmentId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return department;
#pragma warning restore CS8603 // Possible null reference return.
        }


        public async Task<Department> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
#pragma warning disable CS8603 // Possible null reference return.
            if (department == null) return null;
#pragma warning restore CS8603 // Possible null reference return.

            _context.Departments.Remove(department);
            _context.SaveChanges(); return department;


        }
    }
}
